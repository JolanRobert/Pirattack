using Interfaces;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        public PlayerData Data => data;
        public PlayerColor Color => playerSwitchColor.Color;
        public bool IsInteracting => playerInteract.IsInteracting;
        
        public PlayerCollision Collision => playerCollision;
        public PlayerInteract Interact => playerInteract;
        
        [SerializeField] private PlayerData data;
        [SerializeField] private PlayerMovement playerMovement;
        [SerializeField] private PlayerShoot playerShoot;
        [SerializeField] private PlayerSwitchColor playerSwitchColor;
        [SerializeField] private PlayerCollision playerCollision;
        [SerializeField] private PlayerInteract playerInteract;

        private Vector2 moveInput;
        private Vector2 rotateInput;
        private bool shootInput;
        private bool switchColorInput;
        private bool interactInput;

        private void Update()
        {
            HandleMovement();
            HandleRotation();
            HandleShoot();
            HandleSwitchColor();
            HandleInteract();
        }
        
        #region InputCallback
        public void OnMove(InputAction.CallbackContext context)
        {
            moveInput = context.ReadValue<Vector2>().normalized;
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
            switchColorInput = context.performed;
        }

        public void OnInteract(InputAction.CallbackContext context)
        {
            interactInput = context.performed;
        }
        #endregion

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

        private void HandleInteract()
        {
            if (interactInput) playerInteract.BeginInteract();
            else playerInteract.EndInteract();
        }
        
        /*
         * DEBUG
         */
        
        public void RequestSwitchColor()
        {
            playerSwitchColor.Switch();
        }
    }
}