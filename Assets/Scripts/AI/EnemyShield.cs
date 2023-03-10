using Player;
using UnityEngine;
using Utils;

public class EnemyShield : Enemy
{
    public new EnemyShieldData Data;
    
    [SerializeField] private EnemyShieldBT BTShield;

    private void OnEnable()
    {
        healthEnemy.Init(maxHp);
        healthEnemy.onDeath = OnDie;
        if (GameManager.Instance) healthEnemy.onDeath += GameManager.Instance.AddEnemyKilled;
        
        PlayerColor color = (PlayerColor)Random.Range(0, 2);
        AssignShieldColor(color);
        //ChangeShieldRendererColor(color);
        ResetAttackDefaultValue();
        BTShield.ResetBlackboard();
        
        BTShield.enabled = true;
    }
    
    private void OnDisable()
    {
        if (GameManager.Instance) healthEnemy.onDeath -= GameManager.Instance.AddEnemyKilled;
        
        BTShield.enabled = false;
    }

    private void Awake()
    {
        damage = Data.damage; // possible to change damage value
        maxHp = Data.maxHealth; // possible to change max health value
        agent.speed = Data.speed;
    }

    // public void ChangeShieldRendererColor(PlayerColor color)
    // {
    //     switch (color)
    //     {
    //         case PlayerColor.Red:
    //             ShieldRenderer.material.color = new Color(1f, 0f, 0f, 0.5f);
    //             break;
    //         case PlayerColor.Blue:
    //             ShieldRenderer.material.color = new Color(0f, 0f, 1f, 0.5f);
    //             break;
    //         case PlayerColor.None:
    //             ShieldRenderer.material.color = new Color(1f, 1f, 1f, 0.5f);
    //             break;
    //     }
    // }

    protected override void OnDie()
    {
        Pooler.Instance.Depop(Key.EnemyShield, gameObject);
    }
}