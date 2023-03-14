using Player;
using UnityEngine;
using Utils;

public class EnemyShield : Enemy
{
    public new EnemyShieldData Data;
    
    [SerializeField] private EnemyShieldBT BTShield;
    
    private Renderer ShieldRenderer = null;

    private void OnEnable()
    {
        healthEnemy.Init(maxHp);
        healthEnemy.OnDeath = OnDie;
        if (GameManager.Instance) healthEnemy.OnDeath += GameManager.Instance.AddEnemyKilled;
        
        PlayerColor color = (PlayerColor)Random.Range(0, 2);
        AssignShieldColor(color);
        ChangeShieldRendererColor(color);
        ResetAttackDefaultValue();
        BTShield.ResetBlackboard();
        
        BTShield.enabled = true;
    }
    
    private void OnDisable()
    {
        if (GameManager.Instance) healthEnemy.OnDeath -= GameManager.Instance.AddEnemyKilled;
        
        BTShield.enabled = false;
    }

    private void Awake()
    {
        damage = Data.damage; // possible to change damage value
        maxHp = Data.maxHealth; // possible to change max health value
        agent.speed = Data.speed;
    }

    protected override void Depop()
    {
        Pooler.Instance.Depop(Key.EnemyShield, gameObject);
    }

    public void ChangeShieldRendererColor(PlayerColor color)
    {
        if (!ShieldRenderer) ShieldRenderer = GetComponent<Renderer>();
        switch (color)
        {
            case PlayerColor.Red:
                ShieldRenderer.material.color = UnityEngine.Color.red;
                break;
            case PlayerColor.Blue:
                ShieldRenderer.material.color = UnityEngine.Color.blue;
                break;
            case PlayerColor.None:
                ShieldRenderer.material.color = UnityEngine.Color.white;
                break;
        }
    }

    protected override void OnDie()
    {
        Pooler.Instance.Depop(Key.EnemyShield, gameObject);
    }
}