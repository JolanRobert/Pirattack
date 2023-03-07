using System.ComponentModel;
using Player;
using UnityEngine;
using Utils;

public class Bullet : MonoBehaviour
{
    [ReadOnly(true)] public PlayerController owner;

    [SerializeField] private Rigidbody rb;

    private void OnEnable()
    {
        rb.velocity = Vector3.zero;
    }

    public void Init(PlayerData data)
    {
        rb.velocity = transform.forward * data.bulletSpeed;

        Pooler.Instance.DelayedDepop(data.bulletLifespan, Key.Bullet, gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        /*if (other.TryGetComponent(out PlayerController player))
        {
            //Do Nothing
        }*/
        Enemy enemy = other.gameObject.GetComponent<Enemy>();
        if (enemy)
        {
            enemy.TakeDamage(owner.Data.damage, owner);
            Pooler.Instance.Depop(Key.Bullet, gameObject);
        }

        if (!other.GetComponent<PlayerController>())
            Pooler.Instance.Depop(Key.Bullet, gameObject);
    }
}