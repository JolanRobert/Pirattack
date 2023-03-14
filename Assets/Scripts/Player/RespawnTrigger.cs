using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Utils;

namespace Player
{
    public class RespawnTrigger : MonoBehaviour
    {
        [SerializeField] private PlayerRespawn playerRespawn;
        [SerializeField] private HealthBar healthBar;
    
        private HashSet<PlayerController> players = new HashSet<PlayerController>();

        private void OnEnable()
        {
            healthBar.SetRespawnFill(0);
        }

        private void Update()
        {
            if (players.Count == 0) return;

            IncreaseBar();
        }

        private void IncreaseBar()
        {
            float newAmount = healthBar.RespawnAmount + Time.deltaTime / playerRespawn.RespawnDuration;

            Tween tween = healthBar.DoRespawnFill(newAmount, Time.deltaTime);
            if (newAmount >= 1) tween.onComplete += () => playerRespawn.Respawn();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerController player))
            {
                players.Add(player);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out PlayerController player))
            {
                players.Remove(player);
            }
        }
    }
}