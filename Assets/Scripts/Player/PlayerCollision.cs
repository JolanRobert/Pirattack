using Interfaces;
using UnityEngine;

namespace Player
{
    public class PlayerCollision : MonoBehaviour, IDamageable
    {
        [SerializeField] private PlayerController playerController;
        [SerializeField] private Health health;

        private PlayerData data => playerController.Data;

        private void Start()
        {
            health.Init(data.maxHealth);
        }

        public void Damage(int damage)
        {
            health.LoseHealth(damage);
        }
    }
}