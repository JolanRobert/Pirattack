using System;
using Interfaces;
using Player;
using UnityEngine;
using UnityEngine.AI;
using Utils;

public class Enemy : MonoBehaviour, IDamageable
{
    public Action<int, PlayerController> IsWasAttacked;
    public EnemyData Data => enemyData;
    public PlayerColor Color => ShieldColor;
    
    [SerializeField] private EnemyData enemyData;
    [SerializeField] protected PlayerColor ShieldColor = PlayerColor.None;
    [SerializeField] protected Health healthEnemy;
    [SerializeField] protected NavMeshAgent agent;
    [SerializeField] private EnemyBT BT;

    protected int damage = 0;
    protected int maxHp = 0;
    
    private void OnEnable()
    {
        damage = enemyData.damage; // possible to change damage value
        maxHp = enemyData.maxHealth; // possible to change max health value
        healthEnemy.Init((int)maxHp);
        ResetAttackDefaultValue();
        agent.speed = enemyData.speed;
        BT.ResetBlackboard();
        BT.enabled = true;
    }

    private void OnDisable()
    {
        BT.enabled = false;
    }
    
    private void Start()
    {
        healthEnemy.onDeath = OnDie;
        if (GameManager.Instance) healthEnemy.onDeath += GameManager.Instance.AddEnemyKilled;
    }

    public PlayerColor GetShieldColor()
    {
        return ShieldColor;
    }

    protected virtual void OnDie()
    {
        Pooler.Instance.Depop(Key.BasicEnemy, gameObject);
    }

    public void TakeDamage(int _damage, PlayerController origin)
    {
        Damage(_damage);
    }
    
    public void Damage(int damage)
    {
        healthEnemy.LoseHealth(damage);
    }
    
    public void ResetAttackDefaultValue()
    {
        IsWasAttacked = TakeDamage;
    }
    
    public void AssignShieldColor(PlayerColor color)
    {
        ShieldColor = color;
    }

    public void Attack(PlayerController target)
    {
        target.Collision.Damage(damage);
    }
}