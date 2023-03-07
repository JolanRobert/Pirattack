using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerSwitchColor : MonoBehaviour
    {
        public static Action OnSwitchColor;
        public bool Test_With_Single_Player = false;
        
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

        public PlayerColor GetPlayerColor()
        {
            return color;
        }
        
        private void Start()
        {
            if(Test_With_Single_Player)
                color = PlayerColor.Blue;
            else
            color = (PlayerColor)PlayerInputManager.instance.playerCount - 1;
            Debug.Log($"Player {(int)color} -> {color}");
        }

        public void Switch()
        {
            PlayerColor newColor = color == PlayerColor.Blue ? PlayerColor.Red : PlayerColor.Blue;
            Debug.Log($"Player {(int)color} -> {newColor}");
            color = newColor;

        }
    }
}