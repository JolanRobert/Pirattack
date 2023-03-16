using System;
using System.Collections.Generic;
using Interfaces;
using Managers;
using MyBox;
using Player;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using Utils;
using Random = UnityEngine.Random;

namespace AI
{
    public class Enemy : MonoBehaviour, IDamageable
    {
        public Action<int, PlayerColor> IsWasAttacked;
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
        [SerializeField] protected new SkinnedMeshRenderer renderer;
        [SerializeField] private Material[] materials;
        [SerializeField] private float perkChances;
        [SerializeField] public UnityEvent OnShoot;
        [SerializeField][ColorUsageAttribute(true,true)] private Color hurtColor;

        private bool isIced;
        protected bool enemyInVision = false;
        protected int Damagz = 0;
        protected int maxHp = 0;
        protected List<Transform> PatrolPoints = new();
        private EnemyBT bt = null;


        private void OnEnable()
        {
            Damagz = enemyData.damage; // possible to change damage value
            if (GameManager.Instance) InitializeHealth((int)(GameManager.Instance.currentTimer() / 60));
            healthEnemy.Init(maxHp);
            ResetAttackDefaultValue();
            agent.speed = enemyData.speed;
            if (!bt) bt = GetComponent<EnemyBT>();
            bt.ResetBlackboard();
            bt.enabled = true;
            if (GameManager.Instance) GameManager.Instance.OnLaunchingBoss += Depop;
        }

        private void OnDisable()
        {
            bt.enabled = false;
            if (GameManager.Instance) GameManager.Instance.OnLaunchingBoss -= Depop;
        }

        private void Start()
        {
            healthEnemy.OnDeath = OnDie;
            if (GameManager.Instance) healthEnemy.OnDeath += GameManager.Instance.AddEnemyKilled;

            for (int i = 0; i < materials.Length; i++)
            {
                Material copy = new Material(materials[i]);
                materials[i] = copy;
            }
        }

        private void InitializeHealth(int nbMinutes)
        {
            int clampPalier1 = nbMinutes > 3 ? 3 : nbMinutes;
            maxHp = enemyData.maxHealth + enemyData.HealthPalier1 * clampPalier1;
            if (nbMinutes <= 3) return;
            int clampPalier2 = nbMinutes > 6 ? 3 : nbMinutes - 3;
            maxHp += enemyData.HealthPalier2 * clampPalier2;
            if (nbMinutes <= 6) return;
            int clampPalier3 = nbMinutes > 9 ? 3 : nbMinutes - 6;
            maxHp += enemyData.HealthPalier3 * clampPalier3;
            int clampPalier4 = nbMinutes - 9;
            maxHp += enemyData.HealthPalier4 * clampPalier4;
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
            if (Random.Range(0f, 1f) < perkChances)
            {
                GameObject loot = Pooler.Instance.Pop(Pooler.Key.PerkChest);
                loot.transform.position = new Vector3(transform.position.x,0,transform.position.z);
            }
            Pooler.Instance.Depop(Pooler.Key.BasicEnemy, gameObject);
        }

        public void TakeDamage(int _damage, PlayerColor origin)
        {
            Damage(_damage);
            renderer.material.SetColor("_HurtColor",hurtColor);
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
            OnShoot.Invoke();
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
            if (animator && agent) animator.SetFloat("Velocity", agent.velocity.magnitude);
            
            Color color = UnityEngine.Color.Lerp(renderer.material.GetColor("_HurtColor"),UnityEngine.Color.black, 5*Time.deltaTime);
            renderer.material.SetColor("_HurtColor",color);
        }

        public void SetIced(float duration, float slowness)
        {
            if (isIced) return;
            isIced = true;
            renderer.material = materials[1];
            renderer.material.SetColor("_HurtColor",materials[0].GetColor("_HurtColor"));
            GameObject vfx = VFXPooler.Instance.Pop(VFXPooler.Key.PerkIceVFX);
            vfx.transform.position = transform.position;
            VFXPooler.Instance.DelayedDepop(0.5f, VFXPooler.Key.PerkIceVFX, vfx);
            StopIced(duration);
            animator.SetFloat("speedAnimation", slowness);
            agent.speed = enemyData.speed * slowness;
        }

        public async void StopIced(float duration)
        {
            await System.Threading.Tasks.Task.Delay(Mathf.FloorToInt(1000 * duration));
            renderer.material = materials[0];
            renderer.material.SetColor("_HurtColor",materials[1].GetColor("_HurtColor"));
            isIced = false;
            GameObject vfx = VFXPooler.Instance.Pop(VFXPooler.Key.PerkIceVFX);
            vfx.transform.position = transform.position;
            VFXPooler.Instance.DelayedDepop(0.5f, VFXPooler.Key.PerkIceVFX, vfx);
            agent.speed = enemyData.speed;
        }
    }
}