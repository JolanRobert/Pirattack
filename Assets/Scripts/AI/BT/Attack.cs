using System.Collections;
using System.Collections.Generic;
using BehaviourTree;
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
        Player2 target = (Player2)GetData("Target");
        if (target == null) return NodeState.Failure;
        
        caster.Attack(target);
        SetDataInBlackboard("CanAttack", false);
        SetDataInBlackboard("WaitTime", 1f);
        return NodeState.Success;
    }
}
