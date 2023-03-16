using UnityEngine;

namespace AI
{
    [CreateAssetMenu(fileName = "New EnemyShieldData", menuName = "Data/EnemyShieldData")]
    public class EnemyShieldData : ScriptableObject
    {
        [Header("Stats")]
        public int maxHealth;
        public int damage;
        public float attackDistance = 2;
        public float attackSpeed = 1;
        public float speed = 3.5f;
        public float delaySwitchTarget = 0f;
        public float viewRangeDetection = 4f;
        public int HealthPalier1 = 5;
        public int HealthPalier2 = 5;
        public int HealthPalier3 = 5;
        public int HealthPalier4 = 5;

        [Header("Bullets")]
        public float maxSize;
        public float minSize;
        public float speedPattern;
        public float angle;
    }
}
