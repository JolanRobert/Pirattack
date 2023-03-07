using System.Collections;
using System.Collections.Generic;
using BehaviourTree;
using UnityEngine;

public class InitEnemyBlackboard : Node
{
    private bool isInit = false;
    
    public override NodeState Evaluate(Node root)
    {
        if (isInit) return NodeState.Failure;
        isInit = true;
        SetDataInBlackboard("Target", null);
        SetDataInBlackboard("CanAttack", true);
        SetDataInBlackboard("WaitTime", 0f);
        return NodeState.Success;
    }
}

public class InitBossBlackboard : Node
{
    private bool isInit = false;
    
    public override NodeState Evaluate(Node root)
    {
        if (isInit) return NodeState.Failure;
        isInit = true;
        SetDataInBlackboard("WaitTime", 0f);
        return NodeState.Success;
    }
}
