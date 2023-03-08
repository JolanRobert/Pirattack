using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New EnemyData", menuName = "Data/EnemyData")]
public class EnemyData : ScriptableObject
{
    [Header("Stats")]
    public int maxHealth;
    public int damage;

    [Header("Bullets")]
    private float _maxSize;
    private float _minSize;
    private float _speed;
    float _angle;
}
