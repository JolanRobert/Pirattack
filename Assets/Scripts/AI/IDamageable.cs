using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interfaces
{
    public interface IDamageable
    {
        public void TakeDamage(float damage, PlayerColor color = PlayerColor.Undefined);
    }
}
