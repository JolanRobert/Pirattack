using System.Collections;
using System.Collections.Generic;
using BehaviourTree;
using Player;
using UnityEngine;

public class AttackCaC : Node
{
    private Enemy caster;
    public AttackCaC(Enemy _caster)
    {
        caster = _caster;
    }
    
    public override NodeState Evaluate(Node root)
    {
        PlayerController target = GetData<PlayerController>("Target");
        if (target == null) return NodeState.Failure;
        
        caster.Attack(target);
        SetDataInBlackboard("CanAttack", false);
        SetDataInBlackboard("WaitTime", 1f);
        return NodeState.Success;
    }
}
