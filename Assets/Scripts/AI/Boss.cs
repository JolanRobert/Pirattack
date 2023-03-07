using System;
using System.Collections;
using MyBox;
using Player;
using UnityEngine;
using Random = UnityEngine.Random;

public class Boss : Enemy
{
    public static Action<PlayerController> OnTriggerAttack;
    [ReadOnly] public Pattern currentPattern;
    
    [SerializeField] private string[] voicelines;

    public void LaunchPattern(IEnumerator coroutine)
    {
        StartCoroutine(coroutine);
    }


    public void Print_Argh()
    {
        UIManager.Instance.SetVoicelineText(voicelines[Random.Range(0, voicelines.Length)]);
    }
}