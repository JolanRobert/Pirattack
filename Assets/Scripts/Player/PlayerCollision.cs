using Interfaces;
using UnityEngine;
using Utils;

namespace Player
{
    public class PlayerCollision : MonoBehaviour, IDamageable
    {
        [SerializeField] private Health health;

        public void Damage(int damage)
        {
            health.LoseHealth(damage);
        }
    }
}