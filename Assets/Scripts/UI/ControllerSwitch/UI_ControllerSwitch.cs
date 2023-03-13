using System.Collections.Generic;
using DefaultNamespace;
using Player;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

namespace UI
{
    public class UI_ControllerSwitch : MonoBehaviour
    {
        #region Constants
            private const string VE_P1IMG = "VE_P1Img";
            private const string VE_P2IMG = "VE_P2Img";
            private const string LB_P1READY = "LB_P1Ready";
            private const string LB_P2READY = "LB_P2Ready";
        #endregion
        
        [SerializeField] private UIDocument layout;
        [SerializeField] private PlayerDeviceBuffer devicesSO;
        
        public static List<PlayerInUI> Players;

        #region Visual Elements
            private VisualElement p1ImgVE, p2ImgVE;
            private Label p1Ready, p2Ready;
        #endregion

        private bool on;
        
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
           
            // Images
            p1ImgVE = root.Q<VisualElement>(VE_P1IMG);
            p2ImgVE = root.Q<VisualElement>(VE_P2IMG);
            
            // Labels
            p1Ready = root.Q<Label>(LB_P1READY);
            p2Ready = root.Q<Label>(LB_P2READY);
            
            // Default values
            UpdatePlayer(true, null);
            UpdatePlayer(false, null);
        }

        private void Update()
        {
            if (devicesSO.player1Device != null && devicesSO.player2Device != null) Hide();
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
        #endregion
        
        public void TryToJoinPlayer(InputAction.CallbackContext context)
        {
            var device = context.control.device;
            
            if (device is null) return;

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
            private void OnDeviceChange(InputDevice device, InputDeviceChange change)
            {
                if (!on && change is InputDeviceChange.Disconnected)
                {
                    if (devicesSO.player1Device == device || devicesSO.player2Device == device)
                    {
                        Display();
                        return;
                    }
                }
                
                switch (change)
                {
                    case InputDeviceChange.Disconnected:
                        break;
                    case InputDeviceChange.Added:
                        break;
                }
            }
        #endregion
        
        private void Display()
        {
            
        }
        
        private void Hide()
        {
            
        }

    }
}
