using DefaultNamespace;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

namespace UI
{
    public class UI_Lobby : MonoBehaviour
    {
        #region Constants
            private const string VE_P1IMG = "VE_P1Img";
            private const string VE_P2IMG = "VE_P2Img";
            private const string LB_P1READY = "LB_P1Ready";
            private const string LB_P2READY = "LB_P2Ready";
        #endregion
        
        [SerializeField] protected UIDocument layout;
        [SerializeField] protected PlayerDeviceBuffer devicesSO;
        public VisualElement Root => root;
        
        #region Visual Elements
            protected VisualElement root;
            private VisualElement p1ImgVE, p2ImgVE;
            private Label p1Ready, p2Ready;
        #endregion
            
        protected InputDevice p1Device, p2Device;
        
        protected void OnEnable()
        {
            InputSystem.onDeviceChange += OnDeviceChange;
        }

        protected void OnDisable()
        {
            InputSystem.onDeviceChange -= OnDeviceChange;
        }
        
        protected void Init()
        {
            root = layout.rootVisualElement;
        }

        protected void InitVE()
        {
            // Images
            p1ImgVE = root.Q<VisualElement>(VE_P1IMG);
            p2ImgVE = root.Q<VisualElement>(VE_P2IMG);
            
            // Labels
            p1Ready = root.Q<Label>(LB_P1READY);
            p2Ready = root.Q<Label>(LB_P2READY);
        }

        #region UI Update
            protected void DisplayCompletely(bool b)
            {
                p1ImgVE.visible = b;
                p1Ready.visible = b;
                p2ImgVE.visible = b;
                p2Ready.visible = b;
            }
            
            protected void UpdatePlayer(bool p1, InputDevice newDevice)
            {
                if (p1) p1Device = newDevice;
                else p2Device = newDevice;

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
        
        public virtual void TryToJoinPlayer(InputAction.CallbackContext context)
        {
            var device = context.control.device;
            
            if (device is null) return;

            if (device.Equals(p1Device)) UpdatePlayer(true, null);
            else if (device.Equals(p2Device)) UpdatePlayer(false, null);
            else if (p1Device is null) UpdatePlayer(true, device);
            else if (p2Device is null) UpdatePlayer(false, device);
        }
        
        protected virtual void OnDeviceChange(InputDevice device, InputDeviceChange change)
        {
            switch (change)
            {
                case InputDeviceChange.Disconnected:
                    break;
                case InputDeviceChange.Added:
                    break;
            }
        }

    }
}
