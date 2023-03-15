using System;
using System.Collections.Generic;
using MyBox;
using Task;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerInteract : MonoBehaviour
    {
        public bool IsInteracting => isInteracting;

        public Action OnBeginInteract;
        public Action OnEndInteract;

        [SerializeField, ReadOnly] private List<ChaosTask> interactions = new List<ChaosTask>();
        private bool isInteracting;

        private ChaosTask currentInteraction;
        
        private bool LBDown;
        private bool RBDown;
        private bool ADown;
        private bool XDown;
        private bool YDown;
        private Vector2 leftStick;
        private Vector2 rightStick;
        
        #region InputCallback
        public void OnLB(InputAction.CallbackContext context)
        {
            if (context.started) LBDown = true;
        }
        
        public void OnRB(InputAction.CallbackContext context)
        {
            if (context.started) RBDown = true;
        }
        
        public void OnA(InputAction.CallbackContext context)
        {
            if (context.started) ADown = true;
        }
        
        public void OnX(InputAction.CallbackContext context)
        {
            if (context.started) XDown = true;
        }
        
        public void OnY(InputAction.CallbackContext context)
        {
            if (context.started) YDown = true;
        }

        public void OnLeftStick(InputAction.CallbackContext context)
        {
            leftStick = context.ReadValue<Vector2>();
        }

        public void OnRightStick(InputAction.CallbackContext context)
        {
            rightStick = context.ReadValue<Vector2>();
        }
        #endregion

        public void BeginInteract()
        {
            if (interactions.Count == 0) return;
            if (isInteracting) return;
            if (!interactions[0].IsValid()) return;
            
            currentInteraction = interactions[0];
            
            isInteracting = true;
            OnBeginInteract?.Invoke();
        }

        public void UpdateInteract()
        {
            if (!currentInteraction.IsActive)
            {
                EndInteract();
                return;
            }

            ChaosTask task = currentInteraction;
            
            if (!task.IsValid()) return;
            if (task is TaskToilet tToilet) tToilet.HandleInput(LBDown, RBDown);
            else if (task is TaskBedItem tBedItem) tBedItem.HandleInput(ADown);
            else if (task is TaskMustache tMustache) tMustache.HandleInput(leftStick, rightStick);
            else if (task is TaskCauldron tCauldron) tCauldron.HandleInput(ADown, XDown, YDown);
            
            ResetInputs();
        }

        public void EndInteract()
        {
            if (!isInteracting) return;
            isInteracting = false;

            currentInteraction = null;
            OnEndInteract?.Invoke();
        }

        public void Subscribe(ChaosTask elmt)
        {
            interactions.Add(elmt);
        }

        public void Unsubscribe(ChaosTask elmt)
        {
            interactions.Remove(elmt);
        }

        private void ResetInputs()
        {
            LBDown = false;
            RBDown = false;
            ADown = false;
            XDown = false;
            YDown = false;
        }
    }
}