using System;
using System.Collections.Generic;
using InteractiveTrigger;
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

        [SerializeField, ReadOnly] private List<InteractiveElement> interactions = new List<InteractiveElement>();
        private bool isInteracting;

        private InteractiveElement currentInteraction;
        
        private bool LBInput;
        private bool RBInput;
        private bool AInput;
        private Vector2 leftStickInput;
        private Vector2 rightStickInput;
        
        #region InputCallback
        public void OnLB(InputAction.CallbackContext context)
        {
            if (context.started) LBInput = true;
        }
        
        public void OnRB(InputAction.CallbackContext context)
        {
            if (context.started) RBInput = true;
        }
        
        public void OnA(InputAction.CallbackContext context)
        {
            if (context.started) AInput = true;
        }

        public void OnLeftStick(InputAction.CallbackContext context)
        {
            leftStickInput = context.ReadValue<Vector2>();
        }

        public void OnRightStick(InputAction.CallbackContext context)
        {
            rightStickInput = context.ReadValue<Vector2>();
        }
        #endregion

        public void BeginInteract()
        {
            if (interactions.Count == 0) return;
            if (isInteracting) return;

            if (interactions[0] is ChaosTask task)
            {
                if (!task.IsValid()) return;
            }
            
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
            
            if (currentInteraction is RespawnTrigger rTrigger) rTrigger.HandleInput(AInput);
            
            else if (currentInteraction is ChaosTask task)
            {
                if (!task.IsValid()) return;
                if (task is TaskToilet tToilet) tToilet.HandleInput(LBInput, RBInput);
                else if (task is TaskBed tBed) tBed.HandleInput(AInput);
            }
            
            ResetInputs();
        }

        public void EndInteract()
        {
            if (!isInteracting) return;
            isInteracting = false;

            currentInteraction = null;
            OnEndInteract?.Invoke();
        }

        public void Subscribe(InteractiveElement elmt)
        {
            interactions.Add(elmt);
            interactions.Sort();
        }

        public void Unsubscribe(InteractiveElement elmt)
        {
            interactions.Remove(elmt);
        }

        private void ResetInputs()
        {
            LBInput = false;
            RBInput = false;
            AInput = false;
        }
    }
}