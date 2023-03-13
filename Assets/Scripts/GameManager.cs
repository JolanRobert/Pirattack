using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static Action OnLaunchingBoss;
    public static Action OnBossPop;
    public static Action OnEndFightBoss;
    public static Action OnIncreaseChaosBar;
    public static Action OnDecreaseChaosBar;
    
    [SerializeField] private int IncreaseChaosBar = 10;
    [SerializeField] private int DecreaseChaosBar = 10;
    [SerializeField] private GameObject triggerBossDoor;
    
    private float startTime;
    private float endTime;
    private int nbEnemiesKilled = 0;
    private int ChaosBar = 50;
    
    private void Awake()
    {
        Instance = this;
    }
    
    private void Start()
    {
        startTime = Time.time;
        OnIncreaseChaosBar += CheckChaosBar;
        OnDecreaseChaosBar += CheckChaosBar;
        OnLaunchingBoss += LaunchBoss;
        OnEndFightBoss += BossKilled;
        OnBossPop += BossPop;
    }

    public void AddEnemyKilled()
    {
        nbEnemiesKilled++;
    }
    
    public int GetNbEnemiesKilled()
    {
        return nbEnemiesKilled;
    }
    
    public float GetElapsedTimeInGame()
    {
        return endTime / 60f;
    }
    
    private void CheckChaosBar()
    {
        switch (ChaosBar)
        {
            case >= 100:
                OnBossPop?.Invoke();
                break;
            case <= 0:
                UIManager.Instance.ShowEndGame();
                endTime = Time.time - startTime;
                SpawnManager.Instance.enabled = false;
                break;
        }
    }
    
    IEnumerator RelaunchGame()
    {
        yield return new WaitForSeconds(10f);
        SpawnManager.Instance.SetOnBossFight(false);
        ChaosBar = 50;
        OnDecreaseChaosBar?.Invoke();
    }
    
    public void BossKilled()
    {
        StartCoroutine(RelaunchGame());
    }
    
    private void BossPop()
    {
        UIManager.Instance.SetVoicelineText("Boss is here !");
        triggerBossDoor.SetActive(true);
    }
    
    public void LaunchBoss()
    {
        SpawnManager.Instance.SetOnBossFight(true);
    }

    public void SuccessTask()
    {
        if (ChaosBar >= 100) return;
        
        ChaosBar += IncreaseChaosBar;
        OnIncreaseChaosBar?.Invoke();
    }

    public void FailTask()
    {
        if (ChaosBar <= 0) return;
        
        ChaosBar -= DecreaseChaosBar;
        OnDecreaseChaosBar?.Invoke();
    }
    
    public int GetChaosValue()
    {
        return ChaosBar;
    }
    
    public float GetChaosValueRatio()
    {
        return ChaosBar / 100f;
    }


}
