using MyBox;
using Player;
using UnityEngine;
using Utils;

public class Bullet : MonoBehaviour
{
    [ReadOnly] public PlayerController owner;
    [ReadOnly] public bool isPlayerTargeted = false;

    [SerializeField] private Rigidbody rb;

    private void OnEnable()
    {
        rb.velocity = Vector3.zero;
    }

    public void Init(PlayerData data, bool _isPlayerTargeted = false)
    {
        rb.velocity = transform.forward * data.bulletSpeed;
        isPlayerTargeted = _isPlayerTargeted;

        Pooler.Instance.DelayedDepop(data.bulletLifespan, Key.Bullet, gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isPlayerTargeted)
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            if (enemy)
            {
                enemy.TakeDamage(owner.Data.damage, owner);
                Pooler.Instance.Depop(Key.Bullet, gameObject);
            }

            if (!other.GetComponent<PlayerController>())
                Pooler.Instance.Depop(Key.Bullet, gameObject);
        }
        else
        {
            // ADD here the code to detect and damage the player
        }

    }
}