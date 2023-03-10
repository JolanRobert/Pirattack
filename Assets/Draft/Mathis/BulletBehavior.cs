using System;
using System.Collections;
using System.Collections.Generic;
using Interfaces;
using Player;
using UnityEngine;
using Utils;

public class BulletBehavior : MonoBehaviour
{
    public GameObject particleSystem;
    public int bounces;
    public int pierces;
    public int damages;
    public PlayerController owner;
    public WeaponData weaponData;
    public float speed;

    private void Update()
    {
        transform.position += transform.forward * Time.deltaTime * speed;
    }

    public void Init(WeaponData data, PlayerController player)
    {
        weaponData = data;
        owner = player;
        speed = weaponData.bulletSpeed;
        damages = weaponData.damage;
        bounces = weaponData.bounce;
        pierces = weaponData.pierce;
    }

    private void OnCollisionEnter(Collision other)
    {
        // TODO : Remplacer l'instanciation VFX par un Pool

        if (other.transform.CompareTag("Wall"))
        {
            if (bounces > 0)
            {
                Destroy(Instantiate(particleSystem,transform.position,Quaternion.LookRotation(-other.contacts[0].normal)),0.3f);
                Vector2 normal = new Vector2(other.contacts[0].normal.x, other.contacts[0].normal.z);
                Vector2 reflect = Vector2.Reflect(new Vector2(transform.forward.x, transform.forward.z), normal);
                transform.forward = new Vector3(reflect.x, 0, reflect.y);
                bounces--;
            }
            else
            {
                Destroy(Instantiate(particleSystem,transform.position,transform.rotation),0.3f);
                Pooler.Instance.Depop(Key.Bullet, gameObject);
            }   
        }
        
        if (other.transform.CompareTag("Ennemy"))
        {
            if (pierces > 0)
            {
                Destroy(Instantiate(particleSystem,transform.position,transform.rotation),0.3f);
                if (other.gameObject.TryGetComponent(out IDamageable entity))
                {
                    if (entity is PlayerCollision) return;
            
                    if (entity is Enemy) ((Enemy)entity).IsWasAttacked.Invoke(damages, owner);
                    else entity.Damage(damages);
                }
                pierces--;
            }
            else
            {
                Destroy(Instantiate(particleSystem,transform.position,transform.rotation),0.3f);
                if (other.gameObject.TryGetComponent(out IDamageable entity))
                {
                    if (entity is PlayerCollision) return;
            
                    if (entity is Enemy) ((Enemy)entity).IsWasAttacked.Invoke(damages, owner);
                    else entity.Damage(damages);
                }

                Pooler.Instance.Depop(Key.Bullet, gameObject);
            }   
        }
    }

    private void OnEnable()
    {
        //bounces = weaponData.bounce;
        //pierces = weaponData.pierce;
    }
}
