using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New EnemyShieldData", menuName = "Data/EnemyShieldData")]
public class EnemyShieldData : ScriptableObject
{
    [Header("Stats")]
    public int maxHealth;
    public int damage;

    [Header("Bullets")]
    public float maxSize;
    public float minSize;
    public float speed;
    public float angle;
}
