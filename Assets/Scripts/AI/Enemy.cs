using System;
using System.Collections;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] protected float maxHp = 100f;
    [SerializeField] protected float currentHp = 100f;
    [SerializeField] protected float damage = 10f;
    [SerializeField] protected float speed = 1f;
    [SerializeField] protected float attackSpeed = 1f;
    [SerializeField] protected PlayerColor ShieldColor = PlayerColor.Undefined;

    private void OnEnable()
    {
        currentHp = maxHp;
    }
    
    public void TakeDamage(float damage, PlayerColor color = PlayerColor.Undefined)
    {
        if (ShieldColor != PlayerColor.Undefined && ShieldColor != color) return;
        
        currentHp -= damage;
        if (currentHp <= 0)
        {
           gameObject.SetActive(false);
        }
    }
    
    public void AssignShieldColor(PlayerColor color)
    {
        ShieldColor = color;
    }

    public void Attack(Player2 target)
    {
        target.TakeDamage(damage, this);
    }
}