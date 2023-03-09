using System;
using MyBox;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerSwitchColor : MonoBehaviour
    {
        public PlayerColor Color => color;
        public static Action OnSwitchColor;

        [SerializeField] private Renderer sphereRenderer;
        [SerializeField, ReadOnly] private PlayerColor color;

        private void OnEnable()
        {
            OnSwitchColor += Switch;
        }

        private void OnDisable()
        {
            OnSwitchColor -= Switch;
        }
        
        private void Start()
        {
            color = (PlayerColor)PlayerInputManager.instance.playerCount - 1;
            sphereRenderer.material.color = color == PlayerColor.Blue ? UnityEngine.Color.blue : UnityEngine.Color.red;
        }

        public void Switch()
        {
            color = color == PlayerColor.Blue ? PlayerColor.Red : PlayerColor.Blue;
            sphereRenderer.material.color = color == PlayerColor.Blue ? UnityEngine.Color.blue : UnityEngine.Color.red;
        }
    }

    public enum PlayerColor
    {
        Blue, Red, None
    }
}