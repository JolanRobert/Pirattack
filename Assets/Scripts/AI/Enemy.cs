using System;
using System.Collections;
using System.Collections.Generic;
using Interfaces;
using Player;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    public Action<float, PlayerController> IsWasAttacked;
    
    [SerializeField] protected float maxHp = 100f;
    [SerializeField] protected float damage = 10f;
    [SerializeField] protected PlayerColor ShieldColor = PlayerColor.Undefined;
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
    
    public void TakeDamage(float _damage, PlayerController origin)
    {
        if (ShieldColor != PlayerColor.Undefined && ShieldColor != origin.Color) return;
        
        
        IsWasAttacked?.Invoke(_damage, origin);
    }

    public void OnTakeDamage(float _damage, PlayerController origin)
    {
        healthEnemy.LoseHealth((int)_damage);
    }
    
    public void ResetAttackDefaultValue()
    {
        IsWasAttacked = OnTakeDamage;
    }
    
    public void AssignShieldColor(PlayerColor color)
    {
        ShieldColor = color;
    }

    public void Attack(PlayerController target)
    {
        target.TakeDamage(damage, this);
    }
}