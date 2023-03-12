using UnityEngine;

namespace Player
{
    [CreateAssetMenu(fileName = "New PlayerData", menuName = "Data/PlayerData")]
    public class PlayerData : ScriptableObject
    {
        [Header("Stats")]
        public int maxHealth;
        public float moveSpeed;
        
        [Header("Movement")]
        public AnimationCurve moveAcceleration;
        public AnimationCurve moveDeceleration;
        public float rotationSpeed;

        [Header("Other")]
        public float respawnDuration;
        public float switchColorCooldown;
    }
}