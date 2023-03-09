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
            
            private const string USS_BUTTON = "button";
            private const string USS_BUTTONFOCUS = "buttonFocus";
            #endregion
        
        [SerializeField] private UIDocument layout;

        #region Visual Elements
            private Button playBT, quitBT, backBT;
            private VisualElement menuVE, playerConnectionVE;
            private VisualElement p1ImgVE, p2ImgVE;
            private Label p1Ready, p2Ready;
        #endregion

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
                    MenuManager.Instance.player1Device = null;
                    MenuManager.Instance.player2Device = null;
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
        
        private void StartGame()
        {
            
        }

        public void TryToJoinPlayer(InputAction.CallbackContext context)
        {
            if (state is not MenuState.Lobby) return;

            var device = context.control.device;
            var p1Device = MenuManager.Instance.player1Device;
            var p2Device = MenuManager.Instance.player2Device;

            if (device.Equals(p1Device) || device.Equals(p2Device)) return;

            if (p1Device is null)
            {
                p1ImgVE.visible = true;
                MenuManager.Instance.player1Device = device;
            }
            else if (p2Device is null)
            {
                p2ImgVE.visible = true;
                MenuManager.Instance.player2Device = device;
            }
        }
}
}
