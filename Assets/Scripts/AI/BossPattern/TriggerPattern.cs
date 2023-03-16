using Player;
using UnityEngine;
using Utils;

namespace AI.BossPattern
{
    public class TriggerPattern : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if(other.GetComponent<BottleFalling>()) return;
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                Boss.OnTriggerAttack?.Invoke(player);
            }
            GameObject Explosion = VFXPooler.Instance.Pop(VFXPooler.Key.ExplosionVFX);
            Explosion.transform.position = new Vector3(transform.position.x, 0, transform.position.z);
            VFXPooler.Instance.DelayedDepop(2.1f, VFXPooler.Key.ExplosionVFX, Explosion);
            Boss.Instance.currentPattern.EndTrigger(gameObject);
        }
    }
}