using System;
using System.Collections;
using MyBox;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerSwitchColor : MonoBehaviour
    {
        public PlayerColor PColor => color;
        public static Action OnSwitchColor;

        [SerializeField] private PlayerController playerController;
        [SerializeField] private Renderer sphereRenderer;
        [SerializeField, ReadOnly] private PlayerColor color;
        
        private PlayerData data => playerController.Data;

        private bool canSwitch = true;

        private void OnEnable()
        {
            OnSwitchColor += Switch;
        }

        private void OnDisable()
        {
            OnSwitchColor -= Switch;
        }

        public void InitColor(PlayerColor newColor)
        {
            color = newColor;
            sphereRenderer.material.color = color == PlayerColor.Blue ? Color.blue : Color.red;
        }

        public void Switch()
        {
            if (!canSwitch) return;
            
            color = color == PlayerColor.Blue ? PlayerColor.Red : PlayerColor.Blue;
            sphereRenderer.material.color = color == PlayerColor.Blue ? Color.blue : Color.red;
            StartCoroutine(SwitchCooldown());
        }

        private IEnumerator SwitchCooldown()
        {
            canSwitch = false;
            yield return new WaitForSeconds(data.switchColorCooldown);
            canSwitch = true;
        }
    }

    public enum PlayerColor
    {
        Blue, Red, None
    }
}