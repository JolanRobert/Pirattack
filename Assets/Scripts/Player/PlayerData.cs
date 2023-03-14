using UnityEngine;

namespace Player
{
    [CreateAssetMenu(fileName = "New PlayerData", menuName = "Data/PlayerData")]
    public class PlayerData : ScriptableObject
    {
        [Header("Stats")]
        public int maxHealth;
        public int regenValue;
        public float regenTick;
        public float moveSpeed;
        
        [Header("Movement")]
        public AnimationCurve moveAcceleration;
        public AnimationCurve moveDeceleration;
        public float rotationSpeed;

        [Header("Respawn")]
        public float respawnDuration;
        public float respawnStayDuration;
        
        [Header("Other")]
        public float switchColorCooldown;
    }
}