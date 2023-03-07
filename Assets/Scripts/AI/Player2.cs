using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2 : MonoBehaviour
{
    public PlayerColor colorPlayer = PlayerColor.Undefined;
    
    [SerializeField] private float maxHp = 100f;
    [SerializeField] private float currentHp = 100f;
    [SerializeField] private float damage = 10f;

    private void Start()
    {
        currentHp = maxHp;
    }
    
    public float GetDamage()
    {
        return damage;
    }

    public void TakeDamage(float f, Enemy enemy)
    {
        currentHp -= f;
        if (currentHp <= 0)
        {
            //Die
        }
    }
}
