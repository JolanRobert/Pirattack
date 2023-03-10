using System;
using System.Collections;
using DefaultNamespace;
using MyBox;
using Scene;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

namespace UI
{
    public class UI_MainMenu : MonoBehaviour
    {
        private enum MenuState
        {
            Menu,
            Lobby
        }
        
        #region Constants
            private const string BT_PLAY = "BT_Play";
            private const string BT_QUIT = "BT_Quit";
            private const string BT_BACK = "BT_Back";
            private const string VE_MENU = "VE_Menu";
            private const string VE_PLAYERCONNECTION = "VE_PlayerConnection";
            private const string VE_P1IMG = "VE_P1Img";
            private const string VE_P2IMG = "VE_P2Img";
            private const string LB_P1READY = "LB_P1Ready";
            private const string LB_P2READY = "LB_P2Ready";
            
            private const string USS_BUTTON = "button";
            private const string USS_BUTTONFOCUS = "buttonFocus";
        #endregion
        
        [SerializeField] private UIDocument layout;
        [SerializeField] private PlayerInput playerInput;
        [SerializeField] private PlayerDeviceBuffer devicesSO;
        [SerializeField, Scene] private string gameScene;

        #region Visual Elements
            private Button playBT, quitBT, backBT;
            private VisualElement menuVE, playerConnectionVE;
            private VisualElement p1ImgVE, p2ImgVE;
            private Label p1Ready, p2Ready;
        #endregion

        private MenuState state = MenuState.Menu;
        private InputDevice lastMainDevice;

        private void OnEnable()
        {
            InputSystem.onDeviceChange += OnDeviceChange;
        }

        private void OnDisable()
        {
            InputSystem.onDeviceChange -= OnDeviceChange;
        }
        
        private void Start()
        {
            var root = layout.rootVisualElement;

            // Buttons
            playBT = root.Q<Button>(BT_PLAY);
            quitBT = root.Q<Button>(BT_QUIT);
            backBT = root.Q<Button>(BT_BACK);
            
            // Containers
            menuVE = root.Q<VisualElement>(VE_MENU);
            playerConnectionVE = root.Q<VisualElement>(VE_PLAYERCONNECTION);
            
            // Images
            p1ImgVE = root.Q<VisualElement>(VE_P1IMG);
            p2ImgVE = root.Q<VisualElement>(VE_P2IMG);
            
            // Labels
            p1Ready = root.Q<Label>(LB_P1READY);
            p2Ready = root.Q<Label>(LB_P2READY);
            
            //Bindings
            BindButton(playBT, Play, true);
            BindButton(quitBT, Quit, true);
            BindButton(backBT, Back, false);
            
            // Default values
            DisplayMenu(true);
            UpdatePlayer(true, null);
            UpdatePlayer(false, null);
            SetMainDeviceToDefault();
        }

        private void Update()
        {
            if (devicesSO.player1Device != null && devicesSO.player2Device != null) StartGame();
        }

        #region UI Update
            private void UpdatePlayer(bool p1, InputDevice newDevice)
            {
                if (p1) devicesSO.player1Device = newDevice;
                else devicesSO.player2Device = newDevice;

                var visible = newDevice is not null;
                if (p1)
                {
                    p1ImgVE.visible = visible;
                    p1Ready.visible = visible;
                }
                else
                {
                    p2ImgVE.visible = visible;
                    p2Ready.visible = visible;
                }
            }
            
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
        
        public void TryToJoinPlayer(InputAction.CallbackContext context)
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
            
            private void OnDeviceChange(InputDevice device, InputDeviceChange change)
            {
                switch (change)
                {
                    case InputDeviceChange.Disconnected:
                        if (device.Equals(lastMainDevice)) SetMainDeviceToDefault();
                        UpdatePlayer(device.Equals(devicesSO.player1Device), null);
                        break;
                    case InputDeviceChange.Added:
                        MenuManager.Instance.AddPlayer(device);
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
