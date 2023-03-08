using System;
using Interfaces;
using Player;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IDamageable
{
    public Action<int, PlayerController> IsWasAttacked;
    public EnemyData Data => enemyData;
    public PlayerColor Color => ShieldColor;
    
    [SerializeField] private EnemyData enemyData;
    [SerializeField] protected PlayerColor ShieldColor = PlayerColor.None;
    [SerializeField] protected Health healthEnemy;
    [SerializeField] protected NavMeshAgent agent;

    protected int damage = 0;
    protected int maxHp = 0;
    
    private void Start()
    {
        healthEnemy.onDeath += OnDie;
    }

    private void OnEnable()
    {
        damage = enemyData.damage; // possible to change damage value
        maxHp = enemyData.maxHealth; // possible to change max health value
        healthEnemy.Init((int)maxHp);
        ResetAttackDefaultValue();
        agent.speed = enemyData.speed;
    }
    
    public PlayerColor GetShieldColor()
    {
        return ShieldColor;
    }

    protected void OnDie()
    {
        gameObject.SetActive(false);
    }

    public void TakeDamage(int _damage, PlayerController origin)
    {
        //if (ShieldColor != PlayerColor.None && ShieldColor != origin.Color) return;
        
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