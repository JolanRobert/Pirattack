using System;
using System.Collections;
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
            Lobby
        }
        
        #region Constants
            private const string BT_PLAY = "BT_Play";
            private const string BT_QUIT = "BT_Quit";
            private const string VE_MENU = "VE_Menu";
            private const string VE_PLAYERCONNECTION = "VE_PlayerConnection";
            
            private const string USS_BUTTON = "button";
            private const string USS_BUTTONFOCUS = "buttonFocus";
        #endregion
        
        [SerializeField] private PlayerInput playerInput;
        [SerializeField, Scene] private string gameScene;

        #region Visual Elements
            private Button playBT, quitBT, backBT;
            private VisualElement menuVE, playerConnectionVE;
        #endregion

        private MenuState state = MenuState.Menu;
        private InputDevice lastMainDevice;

        private void Start()
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneByName("Menu"));
            
            LobbyManager.Instance.InstantiatePlayers();
            
            Init();
            
            var root = layout.rootVisualElement;
            
            // Buttons
            playBT = root.Q<Button>(BT_PLAY);
            quitBT = root.Q<Button>(BT_QUIT);
            
            // Containers
            menuVE = root.Q<VisualElement>(VE_MENU);
            playerConnectionVE = root.Q<VisualElement>(VE_PLAYERCONNECTION);

            //Bindings
            BindButton(playBT, Play, true);
            BindButton(quitBT, Quit, true);
            
            // Default values
            DisplayMenu(state is MenuState.Menu);
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
            
            private void DisplayMenu(bool b)
            {
                menuVE.style.display = b ? DisplayStyle.Flex : DisplayStyle.None;  
                playerConnectionVE.style.display = b ? DisplayStyle.None : DisplayStyle.Flex;

                if (b)
                {
                    state = MenuState.Menu;
                    playBT.Focus();
                }
                else
                {
                    StartCoroutine(LaunchLobby());
                    UpdatePlayer(true, null);
                    UpdatePlayer(false, null);
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

            private IEnumerator LaunchLobby()
            {
                yield return new WaitForSeconds(0.25f);

                state = MenuState.Lobby;
            }
        #endregion
        
        #region Main Buttons
            private void Play()
            {
                DisplayMenu(false);
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
                DisplayMenu(true);
            }
        #endregion
        
        public override void TryToJoinPlayer(InputAction.CallbackContext context)
        {
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
