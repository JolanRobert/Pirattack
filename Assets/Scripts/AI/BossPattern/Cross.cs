using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

[CreateAssetMenu(fileName = "Cross", menuName = "ScriptableObjects/Patterns/Cross", order = 1)]
public class Cross : Pattern
{
    public override void TouchPlayer(PlayerController player)
    {
        throw new System.NotImplementedException();
    }

    public override void Execute()
    {
        caster.LaunchPattern(CrossCoroutine());
    }

    IEnumerator CrossCoroutine()
    {
        yield return new WaitForSeconds(delay);
    }
}
