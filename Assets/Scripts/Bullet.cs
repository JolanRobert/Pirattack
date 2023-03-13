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

    private Collider lastColliderHit;
    
    private float speed;
    private int damage;
    private int nbBounce;
    private int nbPierce;

    private void OnEnable()
    {
        rb.velocity = Vector3.zero;
        lastColliderHit = null;
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger) return;
        if (other == lastColliderHit) return;

        Debug.Log(other.name);
        
        // TODO : Remplacer l'instanciation VFX par un Pool
        
        //Hit entity
        if (other.TryGetComponent(out IDamageable entity))
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
                if (Physics.Raycast(transform.position-transform.forward, transform.forward, out RaycastHit hit, 1f))
                {
                    Debug.DrawRay(transform.position-transform.forward, transform.forward*1, Color.green, 2f);
                    Destroy(Instantiate(particleSystem, transform.position, Quaternion.LookRotation(-hit.normal)), 0.3f);
                    rb.velocity = Vector3.Reflect(transform.forward, hit.normal) * speed;
                    nbBounce--;
                    return;
                }
                Debug.DrawRay(transform.position-transform.forward, transform.forward*1, Color.red, 2f);
            }
        }

        lastColliderHit = other;
        Destroy(Instantiate(particleSystem,transform.position,transform.rotation),0.3f);
        Pooler.Instance.Depop(Key.Bullet, gameObject);
    }
}