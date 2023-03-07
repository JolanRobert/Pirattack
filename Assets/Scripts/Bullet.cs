using Player;
using UnityEngine;
using Utils;

public class Bullet : MonoBehaviour
{
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

        Pooler.Instance.Depop(Key.Bullet, gameObject);
    }
}