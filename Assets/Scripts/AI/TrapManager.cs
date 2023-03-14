using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapManager : MonoBehaviour
{
    public List<Transform> PatrolPoints = new List<Transform>();

    private List<Enemy> Enemies = new();

    public void AddEnemy(Enemy enemy)
    {
        Enemies.Add(enemy);
    }

    public bool CheckEnemiesVision()
    {
        bool isPlayerInVision = false;
        for (int i = 0; i < Enemies.Count; i++)
        {
            if (Enemies[i].EnemyInVision)
            {
                isPlayerInVision = true;
                break;
            }
        }

        if (isPlayerInVision)
            Enemies.Clear();
        else if (Enemies.Count == 0)
            isPlayerInVision = true;
        return isPlayerInVision;
    }
}