using System;
using System.Collections.Generic;
using MyBox;
using Player;
using UnityEngine;
using UnityEngine.UI;

namespace InteractiveTrigger
{
    public class InteractiveElement : MonoBehaviour, IComparable<InteractiveElement>
    {
        public bool IsActive => isActive;

        [Separator("Interactive Element")]
        [SerializeField] protected Image progressBar;

        protected HashSet<PlayerController> players = new HashSet<PlayerController>();
        private bool isActive;

        protected void OnEnable()
        {
            isActive = true;
            progressBar.fillAmount = 0;
        }
    
        private void OnDisable()
        {
            isActive = false;
            
            foreach (PlayerController player in players)
            {
                player.Interact.Unsubscribe(this);
            }
        
            players.Clear();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerController player))
            {
                players.Add(player);
                player.Interact.Subscribe(this);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out PlayerController player))
            {
                players.Remove(player);
                player.Interact.Unsubscribe(this);
            }
        }

        public int CompareTo(InteractiveElement other)
        {
            return other is RespawnTrigger ? 1 : -1;
        }
    }
}