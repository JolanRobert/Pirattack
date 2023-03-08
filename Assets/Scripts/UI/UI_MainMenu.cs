using System;
using System.Collections;
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
        #endregion
        
        [SerializeField] private UIDocument layout;

        #region Visual Elements
            private Button playBT, quitBT, backBT;
            private VisualElement menuVE, playerConnectionVE;
            private VisualElement p1ImgVE, p2ImgVE;
            private Label p1Ready, p2Ready;
        #endregion

        private PlayerInput playerInputP1, playerInputP2;
        private MenuState state = MenuState.Menu;
        
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
            
            var connectedDeviceCount = 0;
            foreach (var joystickName in InputSystem.devices) {
                Debug.Log(joystickName);
                connectedDeviceCount++;
            }
            Debug.Log("Number of connected devices: " + connectedDeviceCount);
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
                    p1ImgVE.visible = false;
                    p2ImgVE.visible = false;
                    p1Ready.visible = false;
                    p2Ready.visible = false;
                }
            }

            private void FocusButton(Button button, bool focused)
            {
                if (focused)
                {
                    button.RemoveFromClassList("button");
                    button.AddToClassList("buttonFocus");
                }
                else
                {
                    button.RemoveFromClassList("buttonFocus");
                    button.AddToClassList("button");
                }
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
        
        private void StartGame()
        {
            
        }

        /*public void OnJoin(PlayerInput playerInput)
        {
            Debug.Log($"Player joined with {playerInput}");
        }
        
        public void OnLeft(PlayerInput playerInput)
        {
            Debug.Log($"Player left with {playerInput}");
        }*/

        public void OnJoin(InputAction.CallbackContext context)
        {
            if (state is not MenuState.Lobby) return;
            
            if (!playerInputP1)
            {
                playerInputP1 = PlayerInputManager.instance.JoinPlayer();
                p1ImgVE.visible = true;
            }
            else if (!playerInputP2)
            {
                playerInputP2 = PlayerInputManager.instance.JoinPlayer();
                p2ImgVE.visible = true;
            }
        }

        private IEnumerator LaunchLobby()
        {
            yield return new WaitForSeconds(0.25f);

            state = MenuState.Lobby;
        }
    }
}
