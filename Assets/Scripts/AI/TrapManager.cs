using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class TrapManager : MonoBehaviour
{
    public List<Transform> patrolPoints = new List<Transform>();

    private readonly List<Enemy> Enemies = new();

    public void AddEnemy(Enemy enemy)
    {
        Enemies.Add(enemy);
    }

    public bool CheckEnemiesVision()
    {
        bool isPlayerInVision = Enemies.Any(t => t.EnemyInVision);

        if (isPlayerInVision)
            Enemies.Clear();
        else if (Enemies.Count == 0)
            isPlayerInVision = true;
        return isPlayerInVision;
    }
}