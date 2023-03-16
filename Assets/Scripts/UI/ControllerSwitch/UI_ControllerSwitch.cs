using System;
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
            if (GameManager.Instance.GameEnded || !on) return;
            if (p1Device is not null && p2Device is not null) Hide();
        }
        
        #region Input and devices
            protected override void OnDeviceChange(InputDevice device, InputDeviceChange change)
            {
                if (GameManager.Instance.GameEnded) return;
                switch (change)
                {
                    case InputDeviceChange.Disconnected:
                        if (on)
                        {
                            if (device.Equals(p1Device)) UpdatePlayer(true, null);
                            else if (device.Equals(p2Device)) UpdatePlayer(false, null);
                        }
                        else
                        {
                            if (!device.Equals(devicesSO.player1Device) && !device.Equals(devicesSO.player2Device)) return;
                    
                            if (device.Equals(devicesSO.player1Device)) Display(true);
                            else if (device.Equals(devicesSO.player2Device)) Display(false);
                        }
                        break;
                    case InputDeviceChange.Added:
                        if (on) LobbyManager.Instance.AddPlayer(device);
                        break;
                }
            }
        #endregion
        
        private void Display(bool p1Disconnected)
        {
            p1Device = devicesSO.player1Device;
            p2Device = devicesSO.player2Device;
            
            Time.timeScale = 0f;
            pimGame.enabled = false;
            pimUI.enabled = true;
            LobbyManager.Instance.InstantiatePlayers();
            DisplayCompletely(true);
            bgVE.visible = true;
            UpdatePlayer(p1Disconnected, null);
            UpdatePlayer(!p1Disconnected, p1Disconnected ? p2Device : p1Device);
            on = true;
        }
        
        private void Hide()
        {
            devicesSO.player1Device = p1Device;
            devicesSO.player2Device = p2Device;
            
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
