using Interfaces;
using Player;
using UnityEngine;
using Utils;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;

    private int bulletDamage;

    private void OnEnable()
    {
        rb.velocity = Vector3.zero;
    }

    public void Init(PlayerData data)
    {
        rb.velocity = transform.forward * data.bulletSpeed;
        bulletDamage = data.damage;
        
        Pooler.Instance.DelayedDepop(data.bulletLifespan, Key.Bullet, gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IDamageable entity))
        {
            if (entity is not PlayerCollision) entity.Damage(bulletDamage);
        }

        Pooler.Instance.Depop(Key.Bullet, gameObject);
    }
}