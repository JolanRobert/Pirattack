using System;
using System.Collections;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] protected float maxHp = 100f;
    [SerializeField] protected float damage = 10f;
    [SerializeField] protected PlayerColor ShieldColor = PlayerColor.Undefined;
    [SerializeField] protected Health healthPlayer;

    private void Start()
    {
        healthPlayer.onDeath += OnDie;
    }

    private void OnEnable()
    {
        healthPlayer.Init((int)maxHp);
    }

    protected void OnDie()
    {
        gameObject.SetActive(false);
    }
    
    public void TakeDamage(float damage, PlayerColor color = PlayerColor.Undefined)
    {
        if (ShieldColor != PlayerColor.Undefined && ShieldColor != color) return;
        
        healthPlayer.LoseHealth((int)damage);
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