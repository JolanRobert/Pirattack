using System;
using System.Collections;
using MyBox;
using Player;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class Boss : Enemy
{
    public static Action<PlayerController> OnTriggerAttack;
    [ReadOnly] public Pattern currentPattern;
    public new BossData Data;
    
    [SerializeField] private string[] voicelines;

    public void LaunchPattern(IEnumerator coroutine)
    {
        StartCoroutine(coroutine);
    }
    
    private void OnEnable()
    {
        maxHp = Data.maxHealth; // possible to change max health value
        healthEnemy.Init((int)maxHp);
        ResetAttackDefaultValue();
        Print_Argh();
    }

    public void Print_Argh()
    {
        UIManager.Instance.SetVoicelineText(voicelines[Random.Range(0, voicelines.Length)]);
    }
}