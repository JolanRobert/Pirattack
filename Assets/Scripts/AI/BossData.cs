using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New BossData", menuName = "Data/BossData")]
public class BossData : ScriptableObject
{
    [Header("Stats")]
    public int maxHealth;
    public float delayBetWeenPattern = 1f;
    
    [Header("Shield")]
    public int maxHealthShield;

    [Header("Pattern Bottle Rain")]
    public float SpeedBottle = 5f;
    public float ImpactSize = 1f;
    public float MinImpactRange = 1f;
    public float MaxImpactRange = 1f;
    public int NbBottle = 10;
    public int damagePerBottle = 0;
    public float delayBetweenBottle = 0.3f;
    public float delayBeforeFalling = 1.5f;
    


}
