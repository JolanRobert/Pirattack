using System;
using System.Collections;
using MyBox;
using UnityEngine;

namespace Player
{
    public class PlayerSwitchColor : MonoBehaviour
    {
        public PlayerColor PColor => color;
        public static Action OnSwitchColor;

        [SerializeField] private PlayerController playerController;
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
        }

        private void Switch()
        {
            if (!canSwitch) return;
            
            color = color == PlayerColor.Blue ? PlayerColor.Red : PlayerColor.Blue;
            playerController.Interact.EndInteract();
            StartCoroutine(SwitchCooldown());
            
            playerController.Animation.SetTrigger(PlayerAnimation.AnimTrigger.Switch);
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