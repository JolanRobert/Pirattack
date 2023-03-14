using System;
using System.Collections.Generic;
using Interfaces;
using Player;
using UnityEngine;
using UnityEngine.AI;
using Utils;

public class Enemy : MonoBehaviour, IDamageable
{
    public Action<int, PlayerController> IsWasAttacked;
    public EnemyData Data => enemyData;
    public bool EnemyInVision => enemyInVision;
    public Animator Animator => animator;
    public PlayerColor Color => ShieldColor;
    public NavMeshAgent Agent => agent;
    
    [SerializeField] private EnemyData enemyData;
    [SerializeField] protected PlayerColor ShieldColor = PlayerColor.None;
    [SerializeField] protected Health healthEnemy;
    [SerializeField] protected NavMeshAgent agent;
    [SerializeField] protected Animator animator;
    [SerializeField] protected Rigidbody rb;
    [SerializeField] private SkinnedMeshRenderer renderer;
    [SerializeField] private Material[] materials;
    

    protected bool enemyInVision = false;
    protected int damage = 0;
    protected int maxHp = 0;
    protected List<Transform> PatrolPoints = new ();
    private EnemyBT BT = null;
    
    private void OnEnable()
    {
        damage = enemyData.damage; // possible to change damage value
        maxHp = enemyData.maxHealth; // possible to change max health value
        healthEnemy.Init(maxHp);
        ResetAttackDefaultValue();
        agent.speed = enemyData.speed;
        if (!BT) BT = GetComponent<EnemyBT>();
        BT.ResetBlackboard();   
        BT.enabled = true;
        GameManager.OnLaunchingBoss += Depop;
    }

    private void OnDisable()
    {
        BT.enabled = false;
        GameManager.OnLaunchingBoss -= Depop;
    }
    
    private void Start()
    {
        healthEnemy.OnDeath = OnDie;
        if (GameManager.Instance) healthEnemy.OnDeath += GameManager.Instance.AddEnemyKilled;
    }

    protected virtual void Depop()
    {
        Pooler.Instance.Depop(Key.BasicEnemy, gameObject);
    }
    

    public PlayerColor GetShieldColor()
    {
        return ShieldColor;
    }

    protected virtual void OnDie()
    {
        Pooler.Instance.Depop(Key.BasicEnemy, gameObject);
    }

    public void TakeDamage(int _damage, PlayerController origin)
    {
        Damage(_damage);
    }
    
    public void Damage(int damage)
    {
        healthEnemy.LoseHealth(damage);
    }
    
    public void ResetAttackDefaultValue()
    {
        IsWasAttacked = TakeDamage;
    }
    
    public void AssignShieldColor(PlayerColor color)
    {
        ShieldColor = color;
    }

    public void Attack(PlayerController target)
    {
        target.Collision.Damage(damage);
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
        animator.SetFloat("Velocity", agent.velocity.magnitude);
    }

    public void SetIced(float duration)
    {
        renderer.material = materials[1];
        GameObject vfx = Pooler.Instance.Pop(Key.PerkIceVFX);
        vfx.transform.position = transform.position;
        Pooler.Instance.DelayedDepop(0.5f,Key.PerkIceVFX,vfx);
        StopIced(duration);
    }

    public async void StopIced(float duration)
    {
        await System.Threading.Tasks.Task.Delay(Mathf.FloorToInt(1000 * duration));
        renderer.material = materials[0];
        GameObject vfx = Pooler.Instance.Pop(Key.PerkIceVFX);
        vfx.transform.position = transform.position;
        Pooler.Instance.DelayedDepop(0.5f,Key.PerkIceVFX,vfx);
    }
}