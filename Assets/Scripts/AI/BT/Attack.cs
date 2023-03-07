using System.Collections;
using System.Collections.Generic;
using BehaviourTree;
using Player;
using UnityEngine;

public class Attack : Node
{
    private Enemy caster;
    public Attack(Enemy _caster)
    {
        caster = _caster;
    }
    
    public override NodeState Evaluate(Node root)
    {
        PlayerController target = (PlayerController)GetData("Target");
        if (target == null) return NodeState.Failure;
        
        caster.Attack(target);
        SetDataInBlackboard("CanAttack", false);
        SetDataInBlackboard("WaitTime", 1f);
        return NodeState.Success;
    }
}
