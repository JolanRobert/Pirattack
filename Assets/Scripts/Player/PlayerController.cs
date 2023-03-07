using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        public PlayerData Data => data;
        public PlayerColor Color => playerSwitchColor.GetPlayerColor();
        
        [SerializeField] private PlayerData data;
        [SerializeField] private PlayerMovement playerMovement;
        [SerializeField] private PlayerShoot playerShoot;
        [SerializeField] private PlayerSwitchColor playerSwitchColor;
        [SerializeField] private Health healthPlayer;

        private Vector2 moveInput;
        private Vector2 rotateInput;
        private bool shootInput;
        private bool switchColorInput;

        private void Start()
        {
            healthPlayer.Init(data.maxHealth);
        }

        private void Update()
        {
            HandleMovement();
            HandleRotation();
            HandleShoot();
            HandleSwitchColor();
        }
        
        public void OnMove(InputAction.CallbackContext context)
        {
            moveInput = context.ReadValue<Vector2>();
        }
        
        public void OnRotate(InputAction.CallbackContext context)
        {
            rotateInput = context.ReadValue<Vector2>();
        }
        
        public void OnShoot(InputAction.CallbackContext context)
        {
            shootInput = context.performed;
        }

        public void OnSwitchColor(InputAction.CallbackContext context)
        {
            switchColorInput = context.started;
        }
        
        public void RequestSwitchColor()
        {
            playerSwitchColor.Switch();
        }

        private void HandleMovement()
        {
            playerMovement.Move(moveInput);
        }

        private void HandleRotation()
        {
            playerMovement.Rotate(rotateInput);
        }

        private void HandleShoot()
        {
            if (shootInput) playerShoot.Shoot();
        }

        private void HandleSwitchColor()
        {
            if (switchColorInput) PlayerSwitchColor.OnSwitchColor.Invoke();
        }

        public void TakeDamage(float damage, Enemy enemy)
        {
            healthPlayer.LoseHealth((int)damage);
        }
    }
}