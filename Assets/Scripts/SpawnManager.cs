using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private float spawnRate = 5f;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private float delaySpawn = 0.3f;
    
    [SerializeField] private Enemy basicEnemy;
    [SerializeField] private EnemyShield enemyShield;
    
    float StartTime;
    float currentTime;
    int nbSpawn = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        StartTime = Time.time;
        currentTime = 0f;
    }

   
    IEnumerator SpawnEnemy(int nbSpawn)
    {
        for (int i = 0; i < nbSpawn; i++)
        {
            yield return new WaitForSeconds(delaySpawn);
            Instantiate(basicEnemy, spawnPoints[Random.Range(0, spawnPoints.Length)].position, Quaternion.identity).gameObject.SetActive(true);
        }
    }
    
    IEnumerator SpawnShieldEnemy(int nbSpawn)
    {
        for (int i = 0; i < nbSpawn; i++)
        {
            yield return new WaitForSeconds(delaySpawn);
            Instantiate(enemyShield, spawnPoints[Random.Range(0, spawnPoints.Length)].position, Quaternion.identity).gameObject.SetActive(true);
        }
    }
    
    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= spawnRate)
        {
            currentTime = 0f;
            nbSpawn++;
            StartCoroutine(SpawnEnemy(nbSpawn));
            if (nbSpawn % 5 == 0)
                StartCoroutine(SpawnShieldEnemy(1 + nbSpawn / 5));
        }
    }
}
