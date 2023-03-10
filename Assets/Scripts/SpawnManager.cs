using System.Collections;
using System.Collections.Generic;
using MyBox;
using UnityEngine;
using Utils;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private float spawnRate = 5f;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private float delaySpawn = 0.4f;
    [SerializeField] private int nbTickPerShieldSpawn = 5;

    private float currentTime = 0f;
    private int nbSpawn = 0;
    private int nbShield = 0;
    private readonly List<Transform> activeSpawnPoints = new();
    private bool isSpawning = false;
    private List<Key> keys = new();

    IEnumerator SpawnEnemy(Vector3 spawnPosition)
    {
        for (int i = 0; i < keys.Count; i++)
        {
            GameObject enemy = Pooler.Instance.Pop(keys[i]);
            enemy.SetActive(false);
            enemy.transform.position = spawnPosition;
            enemy.SetActive(true);
            yield return new WaitForSeconds(delaySpawn);
        }

        isSpawning = false;
    }

    private void ResetActiveSpawnPoints()
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            activeSpawnPoints.Add(spawnPoints[i]);
        }
    }

    private void CheckAddShieldMan()
    {
        if (nbSpawn % nbTickPerShieldSpawn == 0)
            nbShield++;
    }

    void Update()
    {
        if (isSpawning) return;
        currentTime += Time.deltaTime;
        if (!(currentTime >= spawnRate)) return;

        currentTime = 0f;
        nbSpawn++;
        isSpawning = true;
        keys.Clear();
        if (activeSpawnPoints.Count == 0)
            ResetActiveSpawnPoints();
        int index = Random.Range(0, activeSpawnPoints.Count);
        Vector3 spawnPosition = activeSpawnPoints[index].position;
        activeSpawnPoints.RemoveAt(index);
        CheckAddShieldMan();

        for (int i = 0; i < nbSpawn; i++)
            keys.Add(Key.BasicEnemy);

        for (int i = 0; i < nbShield; i++)
            keys.Add(Key.EnemyShield);

        keys.Shuffle();

        StartCoroutine(SpawnEnemy(spawnPosition));
    }
}