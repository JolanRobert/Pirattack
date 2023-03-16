using MyBox;
using Player;
using UnityEngine;
using Utils;

public class Bullet : MonoBehaviour
{
    public PlayerController Owner => owner;

    [SerializeField] private Rigidbody rb;
    [SerializeField] private AudioSource source;
    [SerializeField] private SphereCollider bulletCollider;
    [SerializeField] private BulletTrigger bulletTrigger;
    [SerializeField, ReadOnly] private PlayerController owner;

    private float speed;
    private int nbBounce;

    private void OnEnable()
    {
        rb.velocity = Vector3.zero;
        bulletCollider.enabled = true;
    }

    public void Init(PlayerController owner, WeaponData data)
    {
        bulletTrigger.Init(data);

        speed = data.bulletSpeed;
        nbBounce = data.nbBounce;

        this.owner = owner;
        rb.velocity = transform.forward * speed;
        Pooler.Instance.DelayedDepop(data.bulletLifespan, Pooler.Key.Bullet, gameObject);
    }

    private void SpawnImpactVFX(Vector3 position, Quaternion rotation)
    {
        GameObject vfx = VFXPooler.Instance.Pop(VFXPooler.Key.BulletImpactVFX);
        vfx.transform.SetPositionAndRotation(position, rotation);
        VFXPooler.Instance.DelayedDepop(0.3f, VFXPooler.Key.BulletImpactVFX, vfx);
    }

    //Layers Player/Enemy EXCLUDED
    private void OnCollisionEnter(Collision collision)
    {
        //Hit wall
        if (nbBounce > 0)
        {
            Vector3 normal = collision.GetContact(0).normal;
            rb.velocity = Vector3.Reflect(transform.forward, normal) * speed;
            rb.MoveRotation(Quaternion.LookRotation(rb.velocity));

            SpawnImpactVFX(transform.position, Quaternion.LookRotation(-normal));
            nbBounce--;
            //source.Play();
            return;
        }

        SpawnImpactVFX(transform.position, transform.rotation);
        Pooler.Instance.Depop(Pooler.Key.Bullet, gameObject);
    }
}