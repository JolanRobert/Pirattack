using System.Collections;
using System.Collections.Generic;
using AI;
using MyBox;
using UnityEngine;
using Utils;

namespace Managers
{
    public class SpawnManager : MonoBehaviour
    {
        public static SpawnManager Instance;

        [SerializeField] private float spawnRate = 5f;
        [SerializeField] private TrapManager[] spawnPoints;
        [SerializeField] private float delaySpawn = 0.4f;
        [SerializeField] private int nbTickPerShieldSpawn = 5;

        private float currentTime = 0f;
        private int nbSpawn = 0;
        private int nbShield = 0;
        private readonly List<TrapManager> activeSpawnPoints = new();
        private bool isSpawning = false;
        private bool onBossFight = false;
        private List<Pooler.Key> keys = new();

        private void Awake()
        {
            Instance = this;
        }

        IEnumerator SpawnEnemy(Vector3 spawnPosition, int index)
        {
            for (int i = 0; i < keys.Count; i++)
            {
                if (onBossFight) yield return null;
                GameObject enemy = Pooler.Instance.Pop(keys[i]);
            
                Enemy enemyScript = enemy.GetComponent<Enemy>();
                activeSpawnPoints[index].AddEnemy(enemyScript);
                enemyScript.SetPatrolPoints(activeSpawnPoints[index].patrolPoints);
                enemy.transform.position = spawnPosition;
                enemy.SetActive(false);
                enemy.SetActive(true);
                yield return new WaitForSeconds(delaySpawn);
            }
            activeSpawnPoints.RemoveAt(index);
            isSpawning = false;
        }

        private void ResetActiveSpawnPoints()
        {
            for (int i = 0; i < spawnPoints.Length; i++)
            {
                activeSpawnPoints.Add(spawnPoints[i]);
            }
        }

        public void SetOnBossFight(bool value)
        {
            onBossFight = value;
        }

        private void CheckAddShieldMan()
        {
            if (nbSpawn % nbTickPerShieldSpawn == 0)
                nbShield++;
        }

        void Update()
        {
            if (isSpawning || onBossFight) return;
            currentTime += Time.deltaTime;
            if (!(currentTime >= spawnRate)) return;

            currentTime = 0f;
            nbSpawn++;
            isSpawning = true;
            keys.Clear();
            if (activeSpawnPoints.Count == 0)
                ResetActiveSpawnPoints();
            int index = Random.Range(0, activeSpawnPoints.Count);
            Vector3 spawnPosition = activeSpawnPoints[index].transform.position;

            bool playerInVision = activeSpawnPoints[index].CheckEnemiesVision();

            if (playerInVision)
            {
                CheckAddShieldMan();

                for (int i = 0; i < nbSpawn; i++)
                    keys.Add(Pooler.Key.BasicEnemy);

                for (int i = 0; i < nbShield; i++)
                    keys.Add(Pooler.Key.EnemyShield);

                keys.Shuffle();
            }
            else
            {
                keys.Add(Pooler.Key.EnemyShield);
            }

            StartCoroutine(SpawnEnemy(spawnPosition, index));
        }
    }
}