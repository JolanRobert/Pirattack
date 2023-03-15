using Interfaces;
using UnityEngine;
using Utils;

namespace Player
{
    public class PlayerCollision : MonoBehaviour, IDamageable
    {
        public Health health => _health;
        
        [SerializeField] private PlayerController playerController;
        [SerializeField] private Health _health;

        public void Damage(int damage)
        {
            _health.LoseHealth(damage);
            playerController.Interact.EndInteract();
        }
    }
}