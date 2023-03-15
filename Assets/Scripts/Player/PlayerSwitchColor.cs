using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using MyBox;
using UnityEngine;

namespace Player
{
    public class PlayerSwitchColor : MonoBehaviour
    {
        public PlayerColor PColor => color;
        public Action OnSwitchColor;

        [SerializeField] private PlayerController playerController;
        [SerializeField] private Renderer parrotRenderer;
        [SerializeField] private Renderer pjRenderer;
        [SerializeField] private Renderer pjHatRenderer;
        [SerializeField, ReadOnly] private PlayerColor color;
        [SerializeField] private Material[] parrotMaterials;
        [SerializeField] private Material[] pjMaterials;
        
        private PlayerData data => playerController.Data;

        private bool canSwitch = true;

        private void Start()
        {
            OnSwitchColor += TrySwitch;
        }

        public void InitColor(PlayerColor newColor)
        {
            color = newColor;
            parrotRenderer.material = color == PlayerColor.Blue ? parrotMaterials[0] : parrotMaterials[1];
            pjRenderer.material = color == PlayerColor.Blue ? pjMaterials[0] : pjMaterials[1];
            pjHatRenderer.material = color == PlayerColor.Blue ? pjMaterials[0] : pjMaterials[1];
        }

        private void TrySwitch()
        {
            if (!canSwitch) return;
            
            List<PlayerController> players = PlayerManager.Players;
            PlayerController other = players[0] == playerController ? players[1] : players[0];
            
            Switch();
            other.Color.Switch();
        }

        private void Switch()
        {
            color = color == PlayerColor.Blue ? PlayerColor.Red : PlayerColor.Blue;
            parrotRenderer.material = color == PlayerColor.Blue ? parrotMaterials[0] : parrotMaterials[1];
            pjRenderer.material = color == PlayerColor.Blue ? pjMaterials[0] : pjMaterials[1];
            pjHatRenderer.material = color == PlayerColor.Blue ? pjMaterials[0] : pjMaterials[1];
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