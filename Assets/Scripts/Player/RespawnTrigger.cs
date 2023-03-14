using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Utils;

namespace Player
{
    public class RespawnTrigger : MonoBehaviour
    {
        [SerializeField] private PlayerController playerController;
        [SerializeField] private PlayerRespawn playerRespawn;
        [SerializeField] private HealthBar healthBar;

        private PlayerData data => playerController.Data;
    
        private HashSet<PlayerController> players = new HashSet<PlayerController>();
        private float timer;

        private void OnEnable()
        {
            healthBar.SetRespawnFill(0);
            timer = data.respawnStayDuration;
        }

        private void Update()
        {
            if (players.Count == 0) timer -= Time.deltaTime;
            else
            {
                timer = data.respawnStayDuration;
                IncreaseBar();
            }
            
            if (timer <= 0) DecreaseBar();
        }

        private void IncreaseBar()
        {
            float newAmount = healthBar.RespawnAmount + Time.deltaTime / data.respawnDuration;

            Tween tween = healthBar.DoRespawnFill(newAmount, Time.deltaTime);
            if (newAmount >= 1) tween.onComplete += () => playerRespawn.Respawn();
        }

        private void DecreaseBar()
        {
            if (healthBar.RespawnAmount == 0) return;

            float newAmount = healthBar.RespawnAmount - Time.deltaTime / data.respawnDuration;
            healthBar.DoRespawnFill(newAmount, Time.deltaTime);
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