using System;
using System.Collections;
using MyBox;
using UnityEngine;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
        public Action OnLaunchingBoss;
        public Action OnBossPop;
        public Action OnEndFightBoss;
        public Action OnRelaunchLoop;
        public Action OnIncreaseChaosBar;
        public Action OnDecreaseChaosBar;
        public Action OnEndGame;
        public bool GameEnded => gameEnded;
        
        [SerializeField] private int increaseChaosBar = 10;
        [SerializeField] private int decreaseChaosBar = 10;
        [SerializeField] private float  depopBossTimer = 60f;
        [SerializeField] private GameObject triggerBossDoor;
        [SerializeField] private GameObject triggerBossExitDoor;
        [SerializeField] private GameObject bossDoor;
        [SerializeField] private GameObject boss;
        [SerializeField, ReadOnly] private int chaosBar = 50;
    
        private float timer;
        private float endTime;
        private int nbEnemiesKilled = 0;
        private bool waitingForBoss = false;
        private float timerDepopBoss;
        private bool gameEnded;

        private void Awake()
        {
            Instance = this;
        }
    
        private void Start()
        {
            timer = 0;
            OnIncreaseChaosBar += CheckChaosBar;
            OnDecreaseChaosBar += CheckChaosBar;
            OnLaunchingBoss += LaunchBoss;
            OnEndFightBoss += BossKilled;
            OnBossPop += BossPop;
            OnEndGame += SetEnd;
        }

        private void SetEnd()
        {
            gameEnded = true;
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
            switch (chaosBar)
            {
                case >= 100:
                    OnBossPop?.Invoke();
                    break;
                case <= 0:
                    EndGame();
                    endTime = Time.time - timer;
                    SpawnManager.Instance.enabled = false;
                    break;
            }
        }
    
        public void EndGame()
        {
            chaosBar = 0;
            OnEndGame?.Invoke();
        }
    
        IEnumerator RelaunchGame(float delay)
        {
            boss.SetActive(false);
            triggerBossDoor.SetActive(false);
            bossDoor.SetActive(false);
            yield return new WaitForSeconds(delay);
            SpawnManager.Instance.SetOnBossFight(false);
            chaosBar = 50;
            OnDecreaseChaosBar?.Invoke();
            waitingForBoss = false;
        
            OnRelaunchLoop?.Invoke();
        }
    
        public void BossKilled()
        {
        
            StartCoroutine(RelaunchGame(10f));
        }
    
        private void BossPop()
        {
            UIManager.Instance.SetVoicelineText("Boss is here !");
            triggerBossDoor.SetActive(true);
            bossDoor.SetActive(false);
            boss.SetActive(true);
            waitingForBoss = true;
            timerDepopBoss = depopBossTimer;
        }
    
        public void LaunchBoss()
        {
            waitingForBoss = false;
            timerDepopBoss = 0f;
            bossDoor.SetActive(true);
            triggerBossExitDoor.SetActive(true);
            SpawnManager.Instance.SetOnBossFight(true);
        }

        public void SuccessTask()
        {
            if (chaosBar >= 100 || waitingForBoss) return;
        
            chaosBar += increaseChaosBar;
            OnIncreaseChaosBar?.Invoke();
        }

        public void FailTask()
        {
            if (chaosBar <= 0 || waitingForBoss) return;
        
            chaosBar -= decreaseChaosBar;
            OnDecreaseChaosBar?.Invoke();
        }
    
        public int GetChaosValue()
        {
            return chaosBar;
        }
    
        public float GetChaosValueRatio()
        {
            return chaosBar / 100f;
        }


        private void Update()
        {
            timer += Time.deltaTime;
            if (!waitingForBoss) return;
            timerDepopBoss -= Time.deltaTime;
            if (timerDepopBoss <= 0f)
            {
                StartCoroutine(RelaunchGame(0f));
            }
        }

        public float currentTimer()
        {
            return timer;
        }

        public void ExitBossDoor()
        {
           triggerBossExitDoor.SetActive(false);
           bossDoor.SetActive(true);
        }
    }
}
