using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AI
{
    public class TrapManager : MonoBehaviour
    {
        public List<Transform> patrolPoints = new ();

        [SerializeField] private Animator animator;
        
        private readonly List<Enemy> enemies = new();

        public void AddEnemy(Enemy enemy)
        {
            enemies.Add(enemy);
            animator.SetTrigger("OpenTrap");
        }

        public bool CheckEnemiesVision()
        {
            bool isPlayerInVision = enemies.Any(t => t.EnemyInVision);

            if (isPlayerInVision)
                enemies.Clear();
            else if (enemies.Count == 0)
                isPlayerInVision = true;
            return isPlayerInVision;
        }
    }
}