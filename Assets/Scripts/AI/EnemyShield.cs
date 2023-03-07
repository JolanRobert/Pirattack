using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShield : Enemy
{
    [SerializeField] protected Renderer ShieldRenderer;
    
    private void OnEnable()
    {
        healthEnemy.Init((int)maxHp);
        PlayerColor color = (PlayerColor)Random.Range(0, 2);
       AssignShieldColor(color);
       ChangeShieldRendererColor(color);
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
            case PlayerColor.Undefined:
                ShieldRenderer.material.color = new Color(1f, 1f, 1f, 0.5f);
                break;
        }
    }
}
