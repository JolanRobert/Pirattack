using Player;
using UnityEngine;
using Utils;

namespace AI
{
    public class EnemyShield : Enemy
    {
        public new EnemyShieldData data;

        [SerializeField] private EnemyShieldBT btShield;

        private Renderer shieldRenderer = null;

        private void OnEnable()
        {
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

        private void OnDisable()
        {
            if (GameManager.Instance) healthEnemy.OnDeath -= GameManager.Instance.AddEnemyKilled;

            btShield.enabled = false;
        }

        private void Awake()
        {
            Damagz = data.damage; // possible to change damage value
            maxHp = data.maxHealth; // possible to change max health value
            agent.speed = data.speed;
        }

        protected override void Depop()
        {
            Pooler.Instance.Depop(Key.EnemyShield, gameObject);
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
            Pooler.Instance.Depop(Key.EnemyShield, gameObject);
        }
    }
}