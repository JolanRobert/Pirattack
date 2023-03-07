using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerSwitchColor : MonoBehaviour
    {
        public static Action OnSwitchColor;
        
        [SerializeField] private PlayerController playerController;
        
        private PlayerColor color;
        private int tmp;

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
            Debug.Log($"Player {(int)color} -> {color}");
        }

        private void Switch()
        {
            PlayerColor newColor = color == PlayerColor.Blue ? PlayerColor.Red : PlayerColor.Blue;
            Debug.Log($"Player {(int)color} -> {newColor}");
            color = newColor;

        }
    }

    public enum PlayerColor
    {
        Blue, Red
    }
}