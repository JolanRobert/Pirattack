using UnityEngine;
using UnityEngine.Serialization;

namespace AI
{
    [CreateAssetMenu(fileName = "New BossData", menuName = "Data/BossData")]
    public class BossData : ScriptableObject
    {
        [Header("Stats")] public int maxHealth;
        public float delayBetweenPattern = 1f;

        [Header("Shield")]
        public int maxHealthShield;
    
        [Header("Loot system")]
        public GameObject[] lootGun;
        public GameObject[] lootGunAmmo;

        [Header("Pattern Bottle Rain")]
        public float speedBottleRain = 5f;
        public float impactSizeRain = 1f;
        public float minImpactRangeRain = 1f;
        public float maxImpactRangeRain = 1f;
        public int nbBottleRain = 10;
        public int damagePerBottleRain = 0;
        public float delayBetweenBottleRain = 0.3f;
        public float delayBeforeFallingRain = 1.5f;

        [Header("Pattern Bottle Harassment")]
        public float SpeedBottleHarassment = 5f;
        public float impactSizeHarassment = 1f;
        public AnimationCurve probabilityDistanceCurve;
        public float ratioDistanceCurve = 0.5f;
        public int nbBottleHarassment = 10;
        public int damagePerBottleHarassment = 0;
        public float delayBetweenBottleHarassment = 0.3f;
        public float delayBeforeFallingHarassment = 1.5f;
    }
}