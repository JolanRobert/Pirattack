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
        owner = null;
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
            switch (entity)
            {
                case PlayerCollision when !owner:
                    // ADD here code for enemy bullet hit player
                    break;
                case Enemy enemy when owner:
                    enemy.IsWasAttacked.Invoke(bulletDamage, owner);
                    break;
            }
        }

        if (!(entity is PlayerCollision && owner))
            Pooler.Instance.Depop(Key.Bullet, gameObject);
    }
}