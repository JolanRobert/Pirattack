using System;
using System.Collections.Generic;
using MyBox;
using Player;
using UnityEngine;

namespace Task
{
    public class TaskTrigger : MonoBehaviour
    {
        public Action OnPlayerEnter;
        
        [SerializeField, ReadOnly] private PlayerColor requiredColor;

        private HashSet<PlayerColor> colors = new HashSet<PlayerColor>();

        public void SetRequiredColor(PlayerColor color)
        {
            requiredColor = color;
        }
        
        public bool Evaluate()
        {
            return colors.Contains(requiredColor);
        }
    
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerSwitchColor player))
            {
                if (colors.Add(player.Color)) OnPlayerEnter.Invoke();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out PlayerSwitchColor player))
            {
                colors.Remove(player.Color);
            }
        }
    }
}
