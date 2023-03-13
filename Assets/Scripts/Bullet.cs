using System;
using Interfaces;
using MyBox;
using Player;
using UnityEngine;
using Utils;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private GameObject particleSystem;
    [SerializeField, ReadOnly] private PlayerController owner;

    private float speed;
    private int damage;
    private int nbBounce;
    private int nbPierce;

    private void OnEnable()
    {
        rb.velocity = Vector3.zero;
    }
    
    public void Init(PlayerController owner)
    {
        this.owner = owner;
        rb.velocity = transform.forward * 10;
        Pooler.Instance.DelayedDepop(5, Key.Bullet, gameObject);
    }

    public void Init(PlayerController owner, WeaponData data)
    {
        speed = data.bulletSpeed;
        damage = data.damage;
        nbBounce = data.nbBounce;
        nbPierce = data.nbPierce;
        
        this.owner = owner;
        rb.velocity = transform.forward * speed;
        Pooler.Instance.DelayedDepop(data.bulletLifespan, Key.Bullet, gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // TODO : Remplacer l'instanciation VFX par un Pool
        
        //Hit entity
        if (collision.gameObject.TryGetComponent(out IDamageable entity))
        {
            //Enemy damage player
            if (entity is PlayerCollision && !owner) entity.Damage(damage);
            
            //Player damage enemy
            else if (entity is Enemy enemy && owner)
            {
                if (nbPierce > 0)
                {
                    enemy.IsWasAttacked.Invoke(damage, owner);
                    Destroy(Instantiate(particleSystem,transform.position,transform.rotation),0.3f);
                    nbPierce--;
                    return;
                }
            }
        }
        
        //Hit wall
        else
        {
            if (nbBounce > 0)
            {
                Vector3 normal = collision.contacts[0].normal;
                Destroy(Instantiate(particleSystem, transform.position, Quaternion.LookRotation(-normal)), 0.3f);
                rb.velocity = Vector3.Reflect(rb.velocity.normalized, normal) * speed;
                nbBounce--;
                return;
            }
        }
        
        Destroy(Instantiate(particleSystem,transform.position,transform.rotation),0.3f);
        Pooler.Instance.Depop(Key.Bullet, gameObject);
    }
}