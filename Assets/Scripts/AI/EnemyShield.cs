using Managers;
using Player;
using UnityEngine;
using UnityEngine.Events;
using Utils;
using Random = UnityEngine.Random;

namespace AI
{
    public class EnemyShield : Enemy
    {
        public EnemyShieldData data;

        [SerializeField] private EnemyShieldBT btShield;
        
        public ParticleSystem shootFX;

        private Renderer shieldRenderer = null;

        private void OnEnable()
        {
            if (GameManager.Instance) InitializeHealth((int)(GameManager.Instance.currentTimer() / 60));
            healthEnemy.Init(maxHp);
            healthEnemy.OnDeath = OnDie;
            if (GameManager.Instance) healthEnemy.OnDeath += GameManager.Instance.AddEnemyKilled;

            PlayerColor color = (PlayerColor)Random.Range(0, 2);
            AssignShieldColor(color);
            ChangeShieldRendererColor(color);
            ResetAttackDefaultValue();
            btShield.ResetBlackboard();

            btShield.enabled = true;
        }
        
        private void InitializeHealth(int nbMinutes)
        {
            int clampPalier1 = nbMinutes > 3 ? 3 : nbMinutes;
            maxHp = data.maxHealth + data.HealthPalier1 * clampPalier1;
            if (nbMinutes <= 3) return;
            int clampPalier2 = nbMinutes > 6 ? 3 : nbMinutes - 3;
            maxHp += data.HealthPalier2 * clampPalier2;
            if (nbMinutes <= 6) return;
            int clampPalier3 = nbMinutes > 9 ? 3 : nbMinutes - 6;
            maxHp += data.HealthPalier3 * clampPalier3;
            int clampPalier4 = nbMinutes - 9;
            maxHp += data.HealthPalier4 * clampPalier4;
        }
        

        private void OnDisable()
        {
            if (GameManager.Instance) healthEnemy.OnDeath -= GameManager.Instance.AddEnemyKilled;

            btShield.enabled = false;
        }

        private void Awake()
        {
            Damagz = data.damage; // possible to change damage value
            agent.speed = data.speed;
        }

        protected override void Depop()
        {
            Pooler.Instance.Depop(Pooler.Key.EnemyShield, gameObject);
        }

        private void ChangeShieldRendererColor(PlayerColor color)
        {
            if (!shieldRenderer) shieldRenderer = GetComponent<Renderer>();
            switch (color)
            {
                case PlayerColor.Red:
                    shieldRenderer.material.color = UnityEngine.Color.red;
                    break;
                case PlayerColor.Blue:
                    shieldRenderer.material.color = UnityEngine.Color.blue;
                    break;
                case PlayerColor.None:
                    shieldRenderer.material.color = UnityEngine.Color.white;
                    break;
            }
        }

        protected override void OnDie()
        {
            GameManager.Instance.AddCoins(data.nbCoinsDropped);
            Pooler.Instance.Depop(Pooler.Key.EnemyShield, gameObject);
        }
        
    }
}