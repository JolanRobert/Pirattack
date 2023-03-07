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
        
        [Header("Debug")]
        public bool Test_With_Single_Player;

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
            if (Test_With_Single_Player) color = PlayerColor.Blue;
            else color = (PlayerColor)PlayerInputManager.instance.playerCount - 1;
        }

        public void Switch()
        {
            color = color == PlayerColor.Blue ? PlayerColor.Red : PlayerColor.Blue;
        }
    }

    public enum PlayerColor
    {
        Blue, Red, None
    }
}