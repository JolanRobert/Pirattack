using MyBox;
using Interfaces;
using Player;
using UnityEngine;
using Utils;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField, ReadOnly] private PlayerController owner;

    private int bulletDamage;

    private void OnEnable()
    {
        rb.velocity = Vector3.zero;
    }

    public void Init(PlayerData data, PlayerController _owner)
    {
        rb.velocity = transform.forward * data.bulletSpeed;

        owner = _owner;
        bulletDamage = data.damage;

        Pooler.Instance.DelayedDepop(data.bulletLifespan, Key.Bullet, gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IDamageable entity))
        {
            if (entity is PlayerCollision && !owner)
            {
                // ADD here code for enemy bullet hit player
            }

            else if (entity is Enemy && owner) ((Enemy)entity).IsWasAttacked.Invoke(bulletDamage, owner);
            else entity.Damage(bulletDamage);
        }

        if (!(entity is PlayerCollision && owner))
            Pooler.Instance.Depop(Key.Bullet, gameObject);
    }
}