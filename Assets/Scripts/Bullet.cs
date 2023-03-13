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
    [SerializeField] private GameObject particleSystem;
    [SerializeField, ReadOnly] private PlayerController owner;

    private float speed;
    private int damage;
    private int nbBounce;
    private int nbPierce;

    private void OnEnable()
    {
        rb.velocity = Vector3.zero;
        collider.enabled = true;
    }

    public void Init(PlayerController owner, WeaponData data)
    {
        bulletTrigger.Init(data);
        
        speed = data.bulletSpeed;
        damage = data.damage;
        nbBounce = data.nbBounce;
        nbPierce = data.nbPierce;
        
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
            Destroy(Instantiate(particleSystem, transform.position, Quaternion.LookRotation(-normal)), 0.3f);
            rb.velocity = Vector3.Reflect(transform.forward, normal) * speed;
            rb.MoveRotation(Quaternion.LookRotation(rb.velocity));
            nbBounce--;
            return;
        }
        
        Destroy(Instantiate(particleSystem,transform.position,transform.rotation),0.3f);
        Debug.Log("collision with "+collision.gameObject.name);
        Pooler.Instance.Depop(Key.Bullet, gameObject);
    }
}