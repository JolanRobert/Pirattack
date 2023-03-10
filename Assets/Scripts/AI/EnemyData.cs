using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New EnemyData", menuName = "Data/EnemyData")]
public class EnemyData : ScriptableObject
{
    [Header("Stats")]
    public int  maxHealth = 100;
    public int damage = 10;
    public float AttackDistance = 2;
    public float speed = 3.5f;
    public float ATKSpeed = 1f;

    [Header("Bullets")]
    private float _maxSize;
    private float _minSize;
    private float _speed;
    float _angle;
}
