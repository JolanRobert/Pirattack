using System;
using System.Collections;
using Player;
using UnityEngine;

[CreateAssetMenu(fileName = "ShieldUp", menuName = "ScriptableObjects/Patterns/ShieldUp", order = 1)]
public class ShieldUp : Pattern
{
    public override void TouchPlayer(PlayerController player)
    {
        throw new NotImplementedException();
    }

    public void ReflectDamage(int _damage, PlayerController origin)
    {
       origin.Collision.Damage(_damage);
    }

    IEnumerator ShieldUpCoroutine()
    {
        caster.IsWasAttacked = ReflectDamage;
        yield return new WaitForSeconds(delay);
        caster.ResetAttackDefaultValue();
    }

    public override void Execute()
    {
        caster.LaunchPattern(ShieldUpCoroutine());
    }
}