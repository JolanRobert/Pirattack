using System;
using Interfaces;
using Player;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    public Action<int, PlayerController> IsWasAttacked;
    
    [SerializeField] protected float maxHp = 100f;
    [SerializeField] protected int damage = 10;
    [SerializeField] protected PlayerColor ShieldColor = PlayerColor.None;
    [SerializeField] protected Health healthEnemy;

    private void Start()
    {
        healthEnemy.onDeath += OnDie;
    }

    private void OnEnable()
    {
        healthEnemy.Init((int)maxHp);
        ResetAttackDefaultValue();
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
        if (ShieldColor != PlayerColor.None && ShieldColor != origin.Color) return;
        
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