using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New EnemyData", menuName = "Data/EnemyData")]
public class EnemyData : ScriptableObject
{
    [Header("Stats")]
    public int  maxHealth = 100;
    public int damage = 10;
    public float attackDistance = 2;
    public float speed = 3.5f;
    public float ATKSpeed = 1f;
    public float viewRangeDetection = 4f;
    public int HealthPalier1 = 5;
    public int HealthPalier2 = 5;
    public int HealthPalier3 = 5;
    public int HealthPalier4 = 5;

    [Header("Bullets")]
    private float _maxSize;
    private float _minSize;
    private float _speed;
    float _angle;
}
