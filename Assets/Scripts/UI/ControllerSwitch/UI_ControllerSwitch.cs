using Managers;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

namespace UI
{
    public class UI_ControllerSwitch : UI_Lobby
    {
        private const string VE_BG = "VE_Bg";
        
        [SerializeField] private PlayerInputManager pimGame, pimUI;
        private bool on;

        private VisualElement bgVE;
        
        private void Start()
        {
            Init();
            InitVE();
            
            bgVE = root.Q<VisualElement>(VE_BG);
            
            Hide();
            
        }

        private void Update()
        {
            if (!on) return;
            if (devicesSO.player1Device is not null && devicesSO.player2Device is not null) Hide();
        }
        
        #region Input and devices
            protected override void OnDeviceChange(InputDevice device, InputDeviceChange change)
            {
                switch (change)
                {
                    case InputDeviceChange.Disconnected:
                        if (!on)
                        {
                            if (!device.Equals(devicesSO.player1Device) && !device.Equals(devicesSO.player2Device)) return;
                    
                            if (device.Equals(devicesSO.player1Device)) Display(true);
                            else if (device.Equals(devicesSO.player2Device)) Display(false);
                    
                            return;
                        }
                        
                        if (device.Equals(devicesSO.player1Device)) UpdatePlayer(true, null);
                        else if (device.Equals(devicesSO.player2Device)) UpdatePlayer(false, null);
                        break;
                    case InputDeviceChange.Added:
                        LobbyManager.Instance.AddPlayer(device);
                        break;
                }
            }
        #endregion
        
        private void Display(bool p1Disconnected)
        {
            Time.timeScale = 0f;
            pimGame.enabled = false;
            pimUI.enabled = true;
            LobbyManager.Instance.InstantiatePlayers();
            DisplayCompletely(true);
            bgVE.visible = true;
            UpdatePlayer(p1Disconnected, null);
            UpdatePlayer(!p1Disconnected, p1Disconnected ? devicesSO.player2Device : devicesSO.player1Device);
            on = true;
        }
        
        private void Hide()
        {
            bgVE.visible = false;
            DisplayCompletely(false);
            LobbyManager.Instance.ClearPlayers();
            pimUI.enabled = false;
            pimGame.enabled = true;
            Time.timeScale = 1f;
            on = false;
        }

    }
}
