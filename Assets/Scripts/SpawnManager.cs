using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private float spawnRate = 5f;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private float delaySpawn = 0.3f;

    [SerializeField] private Enemy basicEnemy;
    [SerializeField] private EnemyShield enemyShield;

    float StartTime;
    float currentTime;
    int nbSpawn = 1;

    // Start is called before the first frame update
    void Start()
    {
        StartTime = Time.time;
        currentTime = 0f;
    }


    IEnumerator SpawnEnemy(int nbSpawn, Vector3 SpawnPosition)
    {
        for (int i = 0; i < nbSpawn; i++)
        {
            yield return new WaitForSeconds(delaySpawn);
            GameObject enemy = Pooler.Instance.Pop(Key.BasicEnemy);
            enemy.transform.position = SpawnPosition;
        }
    }

    IEnumerator SpawnShieldEnemy(int nbSpawn, Vector3 SpawnPosition)
    {
        for (int i = 0; i < nbSpawn; i++)
        {
            yield return new WaitForSeconds(delaySpawn);
            GameObject enemy = Pooler.Instance.Pop(Key.EnemyShield);
            enemy.transform.position = SpawnPosition;
        }
    }

    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= spawnRate)
        {
            currentTime = 0f;
            //nbSpawn++;
            Vector3 SpawnPosition = spawnPoints[Random.Range(0, spawnPoints.Length)].position;
            StartCoroutine(SpawnEnemy(nbSpawn, SpawnPosition));
            if (nbSpawn % 5 == 0)
                StartCoroutine(SpawnShieldEnemy(1 + nbSpawn / 5, SpawnPosition));
        }
    }
}