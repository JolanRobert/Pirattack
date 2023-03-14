using System;
using System.Collections;
using System.Linq;
using MyBox;
using Scene;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace UI
{
    public class UI_MainMenu : UI_Lobby
    {
        private enum MenuState
        {
            Menu,
            Lobby,
            Credits
        }
        
        #region Constants
            private const string VE_BGM = "VE_BackgroundM";
            private const string VE_BGL = "VE_BackgroundL";
            private const string VE_BGC = "VE_BackgroundC";
        
            private const string BT_PLAY = "BT_Play";
            private const string BT_CREDITS = "BT_Credits";
            private const string BT_QUIT = "BT_Quit";
            
            private const string USS_BUTTON = "button";
            private const string USS_BUTTONFOCUS = "buttonFocus";
        #endregion
        
        [SerializeField] private VisualTreeAsset lobbyLayout, creditsLayout;
        [SerializeField] private PlayerInput playerInput;
        [SerializeField, Scene] private string gameScene;

        #region Visual Elements
            private VisualElement menuVE, lobbyVE, creditsVE;
            private Button playBT, creditsBT, quitBT;
        #endregion

        private MenuState state;
        private InputDevice lastMainDevice;

        private void Start()
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneByName("Menu"));
            
            LobbyManager.Instance.InstantiatePlayers();
            
            Init();
            
            var lobbyVEs = lobbyLayout.CloneTree().Children().ToList();
            var creditsVEs = creditsLayout.CloneTree().Children().ToList();
            
            foreach (var visualElement in lobbyVEs)
            {
                root.Add(visualElement);
            }
            foreach (var visualElement in creditsVEs)
            {
                root.Add(visualElement);
            }
            
            InitVE();
            
            // Containers
            menuVE = root.Q<VisualElement>(VE_BGM);
            lobbyVE = root.Q<VisualElement>(VE_BGL);
            creditsVE = root.Q<VisualElement>(VE_BGC);

            // Buttons
            playBT = root.Q<Button>(BT_PLAY);
            creditsBT = root.Q<Button>(BT_CREDITS);
            quitBT = root.Q<Button>(BT_QUIT);

            //Bindings
            BindButton(playBT, Play, true);
            BindButton(creditsBT, Credits, true);
            BindButton(quitBT, Quit, true);
            
            // Default values
            DisplayLayout(MenuState.Menu);
            SetMainDeviceToDefault();
            UpdatePlayer(true, null);
            UpdatePlayer(false, null);
        }

        private void Update()
        {
            if (devicesSO.player1Device != null && devicesSO.player2Device != null) StartGame();
        }

        #region UI Update
            private void BindButton(Button button, Action onClick, bool focusable)
            {
                button.clicked -= onClick;
                button.clicked += onClick;
                
                if (!focusable) return;
                
                button.RegisterCallback<FocusInEvent>(_ => FocusButton(button, true));
                button.RegisterCallback<FocusOutEvent>(_ => FocusButton(button, false));
            }
            
            private void DisplayLayout(MenuState newState)
            {
                state = newState;
                menuVE.style.display = DisplayStyle.None;
                lobbyVE.style.display = DisplayStyle.None;
                creditsVE.style.display = DisplayStyle.None;
                
                switch (state)
                {
                    case MenuState.Menu:
                        menuVE.style.display = DisplayStyle.Flex;
                        playBT.Focus();
                        break;
                    case MenuState.Lobby:
                        lobbyVE.style.display = DisplayStyle.Flex;
                        UpdatePlayer(true, null);
                        UpdatePlayer(false, null);
                        break;
                    case MenuState.Credits:
                        creditsVE.style.display = DisplayStyle.Flex;
                        break;
                }
            }

            private void FocusButton(Button button, bool focused)
            {
                if (focused)
                {
                    button.RemoveFromClassList(USS_BUTTON);
                    button.AddToClassList(USS_BUTTONFOCUS);
                }
                else
                {
                    button.RemoveFromClassList(USS_BUTTONFOCUS);
                    button.AddToClassList(USS_BUTTON);
                }
            }
        #endregion
        
        #region Main Buttons
            private void Play()
            {
                DisplayLayout(MenuState.Lobby);
            }
            
            private void Credits()
            {
                DisplayLayout(MenuState.Credits);
            }
            
            private void Quit()
            {
                #if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;
                #else
                    Application.Quit();
                #endif
            }
            
            public void Back()
            {
                DisplayLayout(MenuState.Menu);
            }
        #endregion
        
        public override void TryToJoinPlayer(InputAction.CallbackContext context)
        {
            Debug.Log("Hey");
            var device = context.control.device;
                
            if (state is not MenuState.Lobby || device is null) return;

            var p1Device = devicesSO.player1Device;
            var p2Device = devicesSO.player2Device;

            if (device.Equals(p1Device))
            {
                UpdatePlayer(true, null);
            } else if (device.Equals(p2Device))
            {
                UpdatePlayer(false, null);
            }
            else if (p1Device is null)
            {
                UpdatePlayer(true, device);
            }
            else if (p2Device is null)
            {
                UpdatePlayer(false, device);
            }
            
        }
        
        #region Input and devices
            private void SetMainDeviceToDefault()
            {
                var gamepads = Gamepad.all;
                if (gamepads.Count <= 0) return;
                playerInput.SwitchCurrentControlScheme(gamepads[0]);
                lastMainDevice = gamepads[0];
            }

            private void SetMainDeviceToOnlyLast()
            {
                if (lastMainDevice is null) return;
                playerInput.SwitchCurrentControlScheme(lastMainDevice);
            }
            
            protected override void OnDeviceChange(InputDevice device, InputDeviceChange change)
            {
                switch (change)
                {
                    case InputDeviceChange.Disconnected:
                        if (device.Equals(lastMainDevice)) SetMainDeviceToDefault();
                        if (device.Equals(devicesSO.player1Device)) UpdatePlayer(true, null);
                        else if (device.Equals(devicesSO.player2Device)) UpdatePlayer(false, null);
                        break;
                    case InputDeviceChange.Added:
                        LobbyManager.Instance.AddPlayer(device);
                        SetMainDeviceToOnlyLast();
                        break;
                }
            }
        #endregion
        
        private void StartGame()
        {
            SceneController.Instance.QuickLoad(gameScene);
        }

    }
}