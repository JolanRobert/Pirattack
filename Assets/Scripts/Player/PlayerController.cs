using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        public PlayerData Data => data;
        public PlayerColor PColor => playerSwitchColor.PColor;
        public bool IsInteracting => playerInteract.IsInteracting;
        public bool IsDown => playerRespawn.IsDown;

        public PlayerSwitchColor Color => playerSwitchColor;
        public PlayerCollision Collision => playerCollision;
        public PlayerInteract Interact => playerInteract;
        public PlayerAnimation Animation => playerAnimation;
        public PlayerStats Stats => playerStats;
        public PlayerShoot Shoot => playerShoot;
        public PlayerMovement Movement => playerMovement;
        public Vector2 MoveInput => moveInput;

        [SerializeField] private PlayerData data;
        [SerializeField] private PlayerMovement playerMovement;
        [SerializeField] private PlayerShoot playerShoot;
        [SerializeField] private PlayerSwitchColor playerSwitchColor;
        [SerializeField] private PlayerCollision playerCollision;
        [SerializeField] private PlayerInteract playerInteract;
        [SerializeField] private PlayerRespawn playerRespawn;
        [SerializeField] private PlayerAnimation playerAnimation;
        [SerializeField] private PlayerStats playerStats;

        private Vector2 moveInput;
        private Vector2 rotateInput;
        private bool shootInput;
        private bool switchColorInput;
        public bool interactInput;
        private bool cancelInteractInput;
        public Action interraction;

        public void Init(Vector3 startPosition, PlayerColor color)
        {
            playerSwitchColor.InitColor(color);
            playerMovement.Init(startPosition);
        }

        private void Update()
        {
            if (AssertState(IsDown)) return;
            
            HandleInteract();

            if (AssertState(IsInteracting)) return;
            
            HandleMovement();
            HandleRotation();
            HandleShoot();
            HandleSwitchColor();
            
            ResetInputs();
        }
        
        #region InputCallback
        public void OnMove(InputAction.CallbackContext context)
        {
            moveInput = context.ReadValue<Vector2>().normalized;
        }
        
        public void OnRotate(InputAction.CallbackContext context)
        {
            rotateInput = context.ReadValue<Vector2>().normalized;
        }
        
        public void OnShoot(InputAction.CallbackContext context)
        {
            shootInput = context.performed;
        }

        public void OnSwitchColor(InputAction.CallbackContext context)
        {
            if (context.started) switchColorInput = true;
        }

        public void OnInteract(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                interactInput = true;
                if(interraction.Target != null)  interraction.Invoke();
            }
        }
        
        public void OnCancelInteract(InputAction.CallbackContext context)
        {
            if (context.started) cancelInteractInput = true;
        }
        #endregion

        private bool AssertState(bool state)
        {
            if (state) playerMovement.Cancel();
            return state;
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
            if (switchColorInput) playerSwitchColor.OnSwitchColor.Invoke();
        }

        private void HandleInteract()
        {
            if (interactInput) playerInteract.BeginInteract();
            if (cancelInteractInput) playerInteract.EndInteract();
            if (IsInteracting) playerInteract.UpdateInteract();
        }

        private void ResetInputs()
        {
            switchColorInput = false;
            interactInput = false;
            cancelInteractInput = false;
        }
    }
}