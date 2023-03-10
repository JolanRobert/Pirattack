using UnityEngine;

namespace Player
{
    [CreateAssetMenu(fileName = "New PlayerData", menuName = "SO/PlayerData")]
    public class PlayerData : ScriptableObject
    {
        [Header("Stats")]
        public int maxHealth;
        public float attackSpeed;
        public float moveSpeed;
        public int damage;
        
        [Header("Movement")]
        public AnimationCurve moveAcceleration;
        public AnimationCurve moveDeceleration;
        public float rotationSpeed;
        
        [Header("Bullets")]
        public float bulletSpeed;
        public float bulletLifespan;

        [Header("Other")]
        public float respawnDuration;
        public float switchColorCooldown;
    }
}