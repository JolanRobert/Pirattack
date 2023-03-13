using Interfaces;
using Player;
using UnityEngine;
using Utils;

public class BulletTrigger : MonoBehaviour
{
    [SerializeField] private Bullet bullet;
    [SerializeField] private SphereCollider collider;
    [SerializeField] private GameObject particleSystem;

    private PlayerController owner => bullet.Owner;

    private float speed;
    private int damage;
    private int nbBounce;
    private int nbPierce;
    
    public void Init(WeaponData data)
    {
        speed = data.bulletSpeed;
        damage = data.damage;
        nbBounce = data.nbBounce;
        nbPierce = data.nbPierce;
    }
    
    //Layers Player/Enemy INCLUDED
    private void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger) return;

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
            
            Destroy(Instantiate(particleSystem,transform.position,transform.rotation),0.3f);
            collider.enabled = false;
            Pooler.Instance.Depop(Key.Bullet, bullet.gameObject);
        }
    }
}
