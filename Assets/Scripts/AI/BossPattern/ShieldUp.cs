using System;
using System.Collections;
using Player;
using UnityEngine;

[CreateAssetMenu(fileName = "ShieldUp", menuName = "ScriptableObjects/Patterns/ShieldUp", order = 1)]
public class ShieldUp : Pattern
{
    public override void TouchPlayer(PlayerController player)
    {
        throw new System.NotImplementedException();
    }

    public void ReflectDamage(float _damage, PlayerController origin)
    {
       origin.TakeDamage(_damage, caster);
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