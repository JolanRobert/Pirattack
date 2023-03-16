using System;
using System.Collections;
using AI.BossPattern;
using Managers;
using MyBox;
using Player;
using UnityEngine;
using UnityEngine.UI;
using Utils;
using Random = UnityEngine.Random;

namespace AI
{
    public class Boss : Enemy
    {
        public static Boss Instance;

        public static Action<PlayerController> OnTriggerAttack;
        [ReadOnly] public Pattern currentPattern;
        public BossData data;

        [SerializeField] private string[] voicelines;
        [SerializeField] private string[] voicelinesDead;
        [SerializeField] private GameObject FXShield;
        [SerializeField] private BossBT bossBt;
        [SerializeField] private Slider HealthBarBoss;
        [SerializeField] private Slider HealthBarShield;

        private int shieldHealth = 0;

        private void Awake()
        {
            Instance = this;
        }


        private void OnEnable()
        {
            if (!GameManager.Instance) return; 
            GameManager.Instance.OnLaunchingBoss += BeginAttack;
            bossBt.enabled = false;
            HealthBarBoss.gameObject.SetActive(false);
            HealthBarShield.gameObject.SetActive(false);
        }

        private void OnDisable()
        {
            if (!GameManager.Instance) return;
            GameManager.Instance.OnLaunchingBoss -= BeginAttack;
        }

        private void BeginAttack()
        {
            maxHp = data.maxHealth; // possible to change max health value
            healthEnemy.Init(maxHp);
            HealthBarBoss.value = healthEnemy.GetRatio();
            HealthBarBoss.gameObject.SetActive(true);
            ResetAttackBossDefaultValue();
            Print_Argh();
            AddShield();
            bossBt.enabled = true;
        }


        public void LaunchPattern(IEnumerator coroutine)
        {
            StartCoroutine(coroutine);
        }

        private void ShieldTakeDamage(int damage, PlayerColor color)
        {
            if (shieldColor != PlayerColor.None && shieldColor != color) return;
            shieldHealth -= damage;
            HealthBarShield.value = ShieldHealthRatio();
            HealthBarShield.gameObject.SetActive(true);
            if (shieldHealth <= 0)
            {
                HealthBarShield.gameObject.SetActive(false);
                ResetAttackBossDefaultValue();
                FXShield.SetActive(false);
            }
        }

        private void TakeBossDamage(int _damage, PlayerColor origin)
        {
            BossDamage(_damage);
        }

        private void BossDamage(int damage)
        {
            float ratio = healthEnemy.GetRatio();
            healthEnemy.LoseHealth(damage);
            HealthBarBoss.value = healthEnemy.GetRatio();
            if (ratio > 0.5f && healthEnemy.GetRatio() <= 0.5f)
            {
                AddShield();
            }
        }

        private void ResetAttackBossDefaultValue()
        {
            IsWasAttacked = TakeBossDamage;
        }

        private void LootSystem()
        {
            for (int i = 0; i < data.nbLoot; i++)
            {
                GameObject loot = Pooler.Instance.Pop(Pooler.Key.PerkLoot);
                loot.transform.SetPositionAndRotation(transform.position + Vector3.back * 10 + Vector3.right * (i - 1),
                    Quaternion.identity);
            }
        }

        protected override void OnDie()
        {
            LootSystem();
            Print_DieVoicelines();
            GameManager.Instance.OnEndFightBoss?.Invoke();
            gameObject.SetActive(false);
        }

        private float ShieldHealthRatio()
        {
            return (float)shieldHealth / (float)data.maxHealthShield;
        }

        private void AddShield()
        {
            shieldColor = (PlayerColor)Random.Range(0, 2);
            shieldHealth = data.maxHealthShield;
            HealthBarShield.value = ShieldHealthRatio();
            FXShield.GetComponent<Renderer>().material.color = (shieldColor == PlayerColor.Blue)
                ? new Color(0, 0, 1, 0.5f)
                : new Color(1, 0, 0, 0.5f);
            HealthBarShield.gameObject.SetActive(true);
            FXShield.SetActive(true);
            IsWasAttacked = ShieldTakeDamage;
        }

        private void Print_Argh()
        {
            UIManager.Instance.SetVoicelineText(voicelines[Random.Range(0, voicelines.Length)]);
        }

        private void Print_DieVoicelines()
        {
            UIManager.Instance.SetVoicelineText(voicelinesDead[Random.Range(0, voicelinesDead.Length)]);
        }
    }
}