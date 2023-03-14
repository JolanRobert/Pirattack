using System;
using Interfaces;
using MyBox;
using Player;
using UnityEngine;
using Utils;

public class Bullet : MonoBehaviour
{
    public PlayerController Owner => owner;
    
    [SerializeField] private Rigidbody rb;
    [SerializeField] private SphereCollider collider;
    [SerializeField] private BulletTrigger bulletTrigger;
    [SerializeField, ReadOnly] private PlayerController owner;

    private float speed;
    private int nbBounce;

    private void OnEnable()
    {
        rb.velocity = Vector3.zero;
        collider.enabled = true;
    }

    public void Init(PlayerController owner, WeaponData data)
    {
        bulletTrigger.Init(data);
        
        speed = data.bulletSpeed;
        nbBounce = data.nbBounce;
        
        this.owner = owner;
        rb.velocity = transform.forward * speed;
        Pooler.Instance.DelayedDepop(data.bulletLifespan, Key.Bullet, gameObject);
    }

    //Layers Player/Enemy EXCLUDED
    private void OnCollisionEnter(Collision collision)
    {
        // TODO : Remplacer l'instanciation VFX par un Pool
        
        //Hit wall
        if (nbBounce > 0)
        {
            Vector3 normal = collision.GetContact(0).normal;
            GameObject vfx = Pooler.Instance.Pop(Key.BulletImpactVFX);
            vfx.transform.position = transform.position;
            vfx.transform.rotation = Quaternion.LookRotation(-normal);
            Pooler.Instance.DelayedDepop(0.3f,Key.BulletImpactVFX,vfx);
            rb.velocity = Vector3.Reflect(transform.forward, normal) * speed;
            rb.MoveRotation(Quaternion.LookRotation(rb.velocity));
            nbBounce--;
            return;
        }
        
        GameObject vfx2 = Pooler.Instance.Pop(Key.BulletImpactVFX);
        vfx2.transform.position = transform.position;
        vfx2.transform.rotation = transform.rotation;
        Pooler.Instance.DelayedDepop(0.3f,Key.BulletImpactVFX,vfx2);
        Debug.Log("collision with "+collision.gameObject.name);
        Pooler.Instance.Depop(Key.Bullet, gameObject);
    }
}