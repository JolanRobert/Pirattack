using System;
using System.Collections.Generic;
using DG.Tweening;
using MyBox;
using Player;
using UnityEngine;
using UnityEngine.UI;

namespace Task
{
    public class TaskTrigger : MonoBehaviour
    {
        [SerializeField] private Image progressBar;
        [SerializeField, ReadOnly] private PlayerColor requiredColor = PlayerColor.None;

        private Task task;
        
        private HashSet<PlayerController> players = new HashSet<PlayerController>();

        private void Awake()
        {
            task = GetComponentInParent<Task>();
        }

        private void OnDisable()
        {
            foreach (PlayerController player in players)
            {
                RemoveInteraction(player);
            }
            
            players.Clear();
        }

        public void IncreaseBar()
        {
            float duration = task.CompletionDuration - progressBar.fillAmount * task.CompletionDuration;
            progressBar.DOKill();
            Tween tween = progressBar.DOFillAmount(1, duration).SetEase(Ease.Linear);
            tween.onComplete += () => task.Complete(this);
        }

        public void DecreaseBar()
        {
            float duration = task.CompletionDuration*2 - (1-progressBar.fillAmount) * task.CompletionDuration*2;
            progressBar.DOKill();
            progressBar.DOFillAmount(0, duration).SetEase(Ease.Linear);
        }
        
        public void SetRequiredColor(PlayerColor color)
        {
            requiredColor = color;
        }
        
        public bool Evaluate()
        {
            foreach (PlayerController player in players)
            {
                if (player.Color == requiredColor && player.IsInteracting) return true;
            }

            return false;
        }

        private void RemoveInteraction(PlayerController player)
        {
            player.Interact.OnBeginInteract -= task.Progress;
            player.Interact.OnEndInteract -= task.DecreaseProgressOvertime;
        }
    
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerController player))
            {
                players.Add(player);
                RemoveInteraction(player);
                player.Interact.OnBeginInteract += task.Progress;
                player.Interact.OnEndInteract += task.DecreaseProgressOvertime;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out PlayerController player))
            {
                players.Remove(player);
                RemoveInteraction(player);
            }
        }
    }
}
