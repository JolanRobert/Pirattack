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
        [Range(0,100)] public float defense;
        public int damage;
        public float resolveDuration;
        
        [Header("Movement")]
        public AnimationCurve moveAcceleration;
        public AnimationCurve moveDeceleration;
        public float rotationSpeed = 0.1f;
        
        [Header("Bullets")]
        public float bulletSpeed;
        public float bulletLifespan;

        [Header("Other")]
        public float respawnDuration;
    }
}