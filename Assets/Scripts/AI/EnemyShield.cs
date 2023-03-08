using Player;
using UnityEngine;

public class EnemyShield : Enemy
{
    public new EnemyShieldData Data;
    
    [SerializeField] protected Renderer ShieldRenderer;
    
    private void OnEnable()
    {
        damage = Data.damage; // possible to change damage value
        maxHp = Data.maxHealth; // possible to change max health value
        agent.speed = Data.speed;
        healthEnemy.Init((int)maxHp);
        PlayerColor color = (PlayerColor)Random.Range(0, 2);
       AssignShieldColor(color);
       ChangeShieldRendererColor(color);
       ResetAttackDefaultValue();
    }
    
    public void ChangeShieldRendererColor(PlayerColor color)
    {
        switch (color)
        {
            case PlayerColor.Red:
                ShieldRenderer.material.color = new Color(1f, 0f, 0f, 0.5f);
                break;
            case PlayerColor.Blue:
                ShieldRenderer.material.color = new Color(0f, 0f, 1f, 0.5f);
                break;
            case PlayerColor.None:
                ShieldRenderer.material.color = new Color(1f, 1f, 1f, 0.5f);
                break;
        }
    }
}
