using System;
using UnityEngine;

namespace Player
{
    public class PlayerInteract : MonoBehaviour
    {
        public bool IsInteracting => isInteracting;
        
        public Action OnBeginInteract;
        public Action OnEndInteract;

        private bool isInteracting;
        
        public void BeginInteract()
        {
            if (isInteracting) return;
            isInteracting = true;
            OnBeginInteract?.Invoke();
        }

        public void EndInteract()
        {
            if (!isInteracting) return;
            isInteracting = false;
            OnEndInteract?.Invoke();
        }
    }
}