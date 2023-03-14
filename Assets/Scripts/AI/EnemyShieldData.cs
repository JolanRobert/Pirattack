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

        [Header("Bullets")]
        public float maxSize;
        public float minSize;
        public float speedPattern;
        public float angle;
        public float viewRangeDetection = 4f;
    }
}
