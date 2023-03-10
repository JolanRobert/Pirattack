using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private float spawnRate = 5f;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private float delaySpawn = 0.4f;
    [SerializeField] private int  nbTickPerShieldSpawn = 5;

    private float currentTime = 0f;
    private int nbSpawn = 0;
    private readonly List<Transform> activeSpawnPoints = new();

    IEnumerator SpawnEnemy(int nbSpawn, Vector3 spawnPosition, Key key)
    {
        for (int i = 0; i < nbSpawn; i++)
        {
            GameObject enemy = Pooler.Instance.Pop(key);
            enemy.transform.position = spawnPosition;
            enemy.SetActive(false);
            enemy.transform.position = spawnPosition;
            enemy.SetActive(true);
            yield return new WaitForSeconds(delaySpawn);
        }
    }

    private void ResetActiveSpawnPoints()
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            activeSpawnPoints.Add(spawnPoints[i]);
        }
    }

    void Update()
    {
        currentTime += Time.deltaTime;
        if (!(currentTime >= spawnRate)) return;
        
        currentTime = 0f;
        nbSpawn++;
        if (activeSpawnPoints.Count == 0)
            ResetActiveSpawnPoints();
        int index = Random.Range(0, activeSpawnPoints.Count);
        Vector3 spawnPosition = spawnPoints[index].position;
        activeSpawnPoints.RemoveAt(index);
        StartCoroutine(SpawnEnemy(nbSpawn, spawnPosition, Key.BasicEnemy));
        if (nbSpawn % nbTickPerShieldSpawn == 0)
            StartCoroutine(SpawnEnemy(1 + nbSpawn / nbTickPerShieldSpawn, spawnPosition, Key.EnemyShield));
    }
}