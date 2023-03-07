using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Boss : Enemy
{
    public static Action<Player2> OnTriggerAttack;
    public Pattern currentPattern;
    
    [SerializeField] private string[] voicelines;

    private void OnEnable()
    {
        OnTriggerAttack = currentPattern.TouchPlayer;
    }


    public void Print_Argh()
    {
        UIManager.Instance.SetVoicelineText(voicelines[Random.Range(0, voicelines.Length)]);
    }
}