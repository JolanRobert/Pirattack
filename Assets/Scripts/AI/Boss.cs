using System;
using System.Collections;
using MyBox;
using Player;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class Boss : Enemy
{
    public static Boss Instance;
    
    public static Action<PlayerController> OnTriggerAttack;
    [ReadOnly] public Pattern currentPattern;
    public new BossData Data;
    
    [SerializeField] private string[] voicelines;
    [SerializeField] private GameObject FXShield;

    private int shieldHealth = 0;

    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        maxHp = Data.maxHealth; // possible to change max health value
        healthEnemy.Init(maxHp);
        ResetAttackDefaultValue();
        Print_Argh();
        AddShield();
    }
    
    public void LaunchPattern(IEnumerator coroutine)
    {
        StartCoroutine(coroutine);
    }

    public void ShieldTakeDamage(int damage, PlayerController origin)
    {
        if (ShieldColor != PlayerColor.None && ShieldColor != origin.Color) return;
        shieldHealth -= damage;
        if (shieldHealth <= 0)
        {
            ResetAttackDefaultValue();
            FXShield.SetActive(false);
        }
    }
    
    public void AddShield()
    {
        ShieldColor = (PlayerColor)Random.Range(0, 2);
        shieldHealth = Data.maxHealthShield;
        FXShield.GetComponent<Renderer>().material.color = (ShieldColor == PlayerColor.Blue)
            ? new Color(0, 0, 1, 0.5f)
            : new Color(1, 0, 0, 0.5f);
        FXShield.SetActive(true);
        IsWasAttacked = ShieldTakeDamage;
    }

    public void Print_Argh()
    {
        UIManager.Instance.SetVoicelineText(voicelines[Random.Range(0, voicelines.Length)]);
    }
}