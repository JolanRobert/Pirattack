using Interfaces;
using UnityEngine;
using Utils;

public class TestEnemy : MonoBehaviour, IDamageable
{
    [SerializeField] private Health health;

    private void Start()
    {
        health.Init(100);
    }
    
    public void Damage(int damage)
    {
        health.LoseHealth(10);
    }
}