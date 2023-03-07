using UnityEngine;
using UnityEngine.UIElements;

namespace UI
{
    public class UI_MainMenu : MonoBehaviour
    {
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
            playBT.clicked -= Play;
            playBT.clicked += Play;
            
            quitBT.clicked -= Quit;
            quitBT.clicked += Quit;
            
            backBT.clicked -= Back;
            backBT.clicked += Back;

            // Default values
            playBT.Focus();
            DisplayMenu(true);
        }

        #region UI Update
            private void DisplayMenu(bool b)
            {
                menuVE.style.display = b ? DisplayStyle.Flex : DisplayStyle.None;  
                playerConnectionVE.style.display = b ? DisplayStyle.None : DisplayStyle.Flex;
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
            
            private void Back()
            {
                DisplayMenu(true);
            }
        #endregion
        
        private void StartGame()
        {
            
        }
    }
}
