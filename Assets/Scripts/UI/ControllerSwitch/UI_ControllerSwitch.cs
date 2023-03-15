using Managers;
using UnityEngine;
using UnityEngine.InputSystem;

namespace UI
{
    public class UI_ControllerSwitch : UI_Lobby
    {
        [SerializeField] private PlayerInputManager pimGame, pimUI;
        private bool on;
        
        private void Start()
        {
            // Default values
            Init();
            InitVE();
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
                if (!on)
                {
                    if (change != InputDeviceChange.Disconnected) return;
                    
                    if (!device.Equals(devicesSO.player1Device) && !device.Equals(devicesSO.player2Device)) return;
                    
                    if (device.Equals(devicesSO.player1Device)) Display(true);
                    else if (device.Equals(devicesSO.player2Device)) Display(false);
                    
                    return;
                }
                
                switch (change)
                {
                    case InputDeviceChange.Disconnected:
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
            UpdatePlayer(p1Disconnected, null);
            UpdatePlayer(!p1Disconnected, p1Disconnected ? devicesSO.player2Device : devicesSO.player1Device);
            on = true;
        }
        
        private void Hide()
        {
            DisplayCompletely(false);
            LobbyManager.Instance.ClearPlayers();
            pimUI.enabled = false;
            pimGame.enabled = true;
            Time.timeScale = 1f;
            on = false;
        }

    }
}
