using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "New BossData", menuName = "Data/BossData")]
public class BossData : ScriptableObject
{
    [Header("Stats")] public int maxHealth;
    public float delayBetweenPattern = 1f;

    [Header("Shield")] public int maxHealthShield;

    [Header("Pattern Bottle Rain")] public float SpeedBottleRain = 5f;
    public float ImpactSizeRain = 1f;
    public float MinImpactRangeRain = 1f;
    public float MaxImpactRangeRain = 1f;
    public int NbBottleRain = 10;
    public int damagePerBottleRain = 0;
    public float delayBetweenBottleRain = 0.3f;
    public float delayBeforeFallingRain = 1.5f;

    [Header("Pattern Bottle Harassment")] public float SpeedBottleHarassment = 5f;
    public float ImpactSizeHarassment = 1f;
    public AnimationCurve probabilityDistanceCurve;
    public float ratioDistanceCurve = 0.5f;
    public int NbBottleHarassment = 10;
    public int damagePerBottleHarassment = 0;
    public float delayBetweenBottleHarassment = 0.3f;
    public float delayBeforeFallingHarassment = 1.5f;
}