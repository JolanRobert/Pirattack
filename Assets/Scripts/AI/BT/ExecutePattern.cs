using System.Collections;
using System.Collections.Generic;
using BehaviourTree;
using UnityEngine;

public class ExecutePattern : Node
{
    public override NodeState Evaluate(Node root)
    {
        Pattern pattern = GetData<Pattern>("currentPattern");
        Boss.OnTriggerAttack = pattern.TouchPlayer;
        if (pattern == null) return NodeState.Failure;
        pattern.Execute();
        return NodeState.Success;
    }
}
