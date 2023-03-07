using MyBox;
using Interfaces;
using Player;
using UnityEngine;
using Utils;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField, ReadOnly] private PlayerController owner;
    [SerializeField, ReadOnly] private bool isPlayerTargeted = false;
    
    private int bulletDamage;

    private void OnEnable()
    {
        rb.velocity = Vector3.zero;
    }

    public void Init(PlayerData data, PlayerController owner, bool _isPlayerTargeted = false)
    {
        rb.velocity = transform.forward * data.bulletSpeed;

        this.owner = owner;
        bulletDamage = data.damage;
        isPlayerTargeted = _isPlayerTargeted;
        
        Pooler.Instance.DelayedDepop(data.bulletLifespan, Key.Bullet, gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isPlayerTargeted) return;
        
        if (other.TryGetComponent(out IDamageable entity))
        {
            if (entity is PlayerCollision) return;
            
            if (entity is Enemy) ((Enemy)entity).IsWasAttacked.Invoke(bulletDamage, owner);
            else entity.Damage(bulletDamage);
        }

        Pooler.Instance.Depop(Key.Bullet, gameObject);
    }
}