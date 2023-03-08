using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New EnemyShieldData", menuName = "Data/EnemyShieldData")]
public class EnemyShieldData : ScriptableObject
{
    [Header("Stats")]
    public int maxHealth;
    public int damage;
    public float AttackDistance = 2;
    public float speed = 3.5f;

    [Header("Bullets")]
    public float maxSize;
    public float minSize;
    public float speedPattern;
    public float angle;
}
