using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerSwitchColor : MonoBehaviour
    {
        public PlayerColor Color => color;
        public static Action OnSwitchColor;
        
        [SerializeField] private PlayerController playerController;
        
        private PlayerColor color;
        private PlayerData data => playerController.Data;

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
        }

        private void Switch()
        {
            color = color == PlayerColor.Blue ? PlayerColor.Red : PlayerColor.Blue;
        }
    }

    public enum PlayerColor
    {
        Blue, Red
    }
}