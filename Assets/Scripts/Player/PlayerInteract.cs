using System;
using System.Collections.Generic;
using MyBox;
using UnityEngine;

namespace Player
{
    public class PlayerInteract : MonoBehaviour
    {
        public bool IsInteracting => isInteracting;
        
        public Action OnEndInteract;

        [SerializeField, ReadOnly] private List<InteractiveElement> interactions = new List<InteractiveElement>();
        private bool isInteracting;
        
        public void BeginInteract()
        {
            if (interactions.Count == 0) return;
            
            interactions[0].Progress();
            isInteracting = true;
        }

        public void EndInteract()
        {
            if (!isInteracting) return;
            isInteracting = false;
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
    }
}