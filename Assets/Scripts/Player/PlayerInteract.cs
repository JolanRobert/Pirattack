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
        [SerializeField, ReadOnly] private bool isInteracting;

        [SerializeField] private GameObject imageA;
        [SerializeField] private GameObject imageX;
        [SerializeField] private GameObject imageY;

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
            if (task is TaskToilet tToilet)
            {
                //L ou R
                tToilet.HandleInput(LBDown, RBDown);
            }
            else if (task is TaskBedItem tBedItem)
            {
                imageA.SetActive(true);
                tBedItem.HandleInput(ADown);
            }
            else if (task is TaskMustache tMustache)
            {
                //Joystick
                tMustache.HandleInput(leftStick, rightStick);
            }
            else if (task is TaskCauldron tCauldron)
            {
                TaskCauldron.CauldronInput nextInput = tCauldron.NextInput;
                if (nextInput == TaskCauldron.CauldronInput.A)
                {
                    imageA.SetActive(true);
                    imageX.SetActive(false);
                    imageY.SetActive(false);
                }
                else if (nextInput == TaskCauldron.CauldronInput.X)
                {
                    imageA.SetActive(false);
                    imageX.SetActive(true);
                    imageY.SetActive(false);
                }
                else if (nextInput == TaskCauldron.CauldronInput.Y)
                {
                    imageA.SetActive(false);
                    imageX.SetActive(false);
                    imageY.SetActive(true);
                }
                
                tCauldron.HandleInput(ADown, XDown, YDown);
            }
            
            ResetInputs();
        }

        public void EndInteract()
        {
            if (!isInteracting) return;
            isInteracting = false;

            DisableImages();

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

        private void DisableImages()
        {
            imageA.SetActive(false);
            imageX.SetActive(false);
            imageY.SetActive(false);
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