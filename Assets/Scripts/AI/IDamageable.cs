using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

namespace Interfaces
{
    public interface IDamageable
    {
        public void TakeDamage(float _damage, PlayerController origin);
    }
}
