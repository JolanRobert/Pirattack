using Interfaces;
using Player;
using UnityEngine;

namespace AI
{
    public class WaveTrigger : MonoBehaviour
    {
        [SerializeField] private ConeProjectile coneProjectile;
    
        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out IDamageable entity)) return;
            if (entity is not PlayerCollision) return;
            if (other.GetComponent<PlayerController>().Color.PColor != coneProjectile.Caster.Color)
            {
                entity.Damage(coneProjectile.Caster.data.damage);
            }
        }
    }
}
