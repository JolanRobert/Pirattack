using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New BossData", menuName = "Data/BossData")]
public class BossData : ScriptableObject
{
    [Header("Stats")]
    public int maxHealth;
    public float delayBetWeenPattern = 1f;

    [Header("Pattern Bottle Rain")]
    public float SpeedBottle = 5f;
    public float ImpactSize = 1f;
    public float NbBottle = 10f;
    public float damagePerBottle = 0f;
}
