using System;
using System.Collections.Generic;
using Interfaces;
using Managers;
using MyBox;
using Player;
using UnityEngine;
using UnityEngine.AI;
using Utils;

namespace AI
{
    public class Enemy : MonoBehaviour, IDamageable
    {
        public Action<int, PlayerController> IsWasAttacked;
        public EnemyData Data => enemyData;
        public bool EnemyInVision => enemyInVision;
        public Animator Animator => animator;
        public PlayerColor Color => shieldColor;
        public NavMeshAgent Agent => agent;
    
        [SerializeField] private EnemyData enemyData;
        [SerializeField] protected PlayerColor shieldColor = PlayerColor.None;
        [SerializeField] protected Health healthEnemy;
        [SerializeField] protected NavMeshAgent agent;
        [SerializeField] protected Animator animator;
        [SerializeField] protected Rigidbody rb;
        [SerializeField] private new SkinnedMeshRenderer renderer;
        [SerializeField] private Material[] materials;
    

        protected bool enemyInVision = false;
        protected int Damagz = 0;
        [SerializeField, ReadOnly] protected int maxHp = 0;
        protected List<Transform> PatrolPoints = new ();
        private EnemyBT bt = null;
    
        
        
        private void OnEnable()
        {
            Damagz = enemyData.damage; // possible to change damage value
            InitializeHealth((int)(GameManager.Instance.currentTimer() / 60));
            healthEnemy.Init(maxHp);
            ResetAttackDefaultValue();
            agent.speed = enemyData.speed;
            if (!bt) bt = GetComponent<EnemyBT>();
            bt.ResetBlackboard();   
            bt.enabled = true;
            GameManager.OnLaunchingBoss += Depop;
        }

        private void OnDisable()
        {
            bt.enabled = false;
            GameManager.OnLaunchingBoss -= Depop;
        }
    
        private void Start()
        {
            healthEnemy.OnDeath = OnDie;
            if (GameManager.Instance) healthEnemy.OnDeath += GameManager.Instance.AddEnemyKilled;
        }

        private void InitializeHealth(int nbMinutes)
        {
            int clampPalier1 = nbMinutes > 3 ? 3 : nbMinutes;
            maxHp = enemyData.maxHealth + enemyData.maxHealth * clampPalier1;
            if (nbMinutes <= 3) return;
            int clampPalier2 = nbMinutes > 6 ? 3 : nbMinutes - 3;
            maxHp += enemyData.maxHealth * clampPalier2;
            if (nbMinutes <= 6) return;
            int clampPalier3 = nbMinutes > 9 ? 3 : nbMinutes - 6;
            maxHp += enemyData.maxHealth * clampPalier3;
            int clampPalier4 = nbMinutes - 9;
            maxHp += enemyData.maxHealth * clampPalier4;
        }

        protected virtual void Depop()
        {
            Pooler.Instance.Depop(Pooler.Key.BasicEnemy, gameObject);
        }
    

        public PlayerColor GetShieldColor()
        {
            return shieldColor;
        }

        protected virtual void OnDie()
        {
            Pooler.Instance.Depop(Pooler.Key.BasicEnemy, gameObject);
        }

        public void TakeDamage(int _damage, PlayerController origin)
        {
            Damage(_damage);
        }
    
        public void Damage(int _damage)
        {
            healthEnemy.LoseHealth(_damage);
        }
    
        public void ResetAttackDefaultValue()
        {
            IsWasAttacked = TakeDamage;
        }
    
        public void AssignShieldColor(PlayerColor color)
        {
            shieldColor = color;
        }

        public void Attack(PlayerController target)
        {
            target.Collision.Damage(Damagz);
        }

        public void OnPlayerOnVision()
        {
            enemyInVision = true;
        }
    
        public List<Transform> GetPatrolPoints()
        {
            return PatrolPoints;
        }
    
        public void SetPatrolPoints(List<Transform> points)
        {
            PatrolPoints.Clear();
            for (int i = 0; i < points.Count; i++)
            {
                PatrolPoints.Add(points[i]);
            }
        }

        private void Update()
        {
            animator.SetFloat("Velocity", agent.velocity.magnitude);
        }

        public void SetIced(float duration)
        {
            renderer.material = materials[1];
            GameObject vfx = VFXPooler.Instance.Pop(VFXPooler.Key.PerkIceVFX);
            vfx.transform.position = transform.position;
            VFXPooler.Instance.DelayedDepop(0.5f,VFXPooler.Key.PerkIceVFX,vfx);
            StopIced(duration);
        }

        public async void StopIced(float duration)
        {
            await System.Threading.Tasks.Task.Delay(Mathf.FloorToInt(1000 * duration));
            renderer.material = materials[0];
            GameObject vfx = VFXPooler.Instance.Pop(VFXPooler.Key.PerkIceVFX);
            vfx.transform.position = transform.position;
            VFXPooler.Instance.DelayedDepop(0.5f,VFXPooler.Key.PerkIceVFX,vfx);
        }
    }
}