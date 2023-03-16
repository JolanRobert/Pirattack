using Managers;
using MyBox;
using Scene;
using UnityEngine;
using UnityEngine.UIElements;
using Utils;

namespace UI
{
    public class UI_PauseScreen : MonoBehaviour
    {
        private const string VE_BG = "VE_Bg";
        private const string BT_MENU = "BT_Menu";

        [SerializeField] private UIDocument layout;
        [SerializeField, Scene] private string menuScene;

        private VisualElement bgVE;
        private Button btMenu;

        private bool on;

        private void Start()
        {
            var root = layout.rootVisualElement;

            bgVE = root.Q<VisualElement>(VE_BG);
            btMenu = root.Q<Button>(BT_MENU);

            Utilities.BindButton(btMenu, ToMenu, true, true);
            Hide();
        }

        private void ToMenu()
        {
            SceneController.Instance.QuickLoad(menuScene);
        }

        public void TogglePause()
        {
            if (GameManager.Instance.GameEnded) return;
            if (on) Hide();
            else Display();
        }
        
        private void Display()
        {
            Time.timeScale = 0f;
            bgVE.visible = true;
        }

        private void Hide()
        {
            bgVE.visible = false;
            Time.timeScale = 1f;
        }
    }
}
