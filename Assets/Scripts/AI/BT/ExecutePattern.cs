using System.Collections;
using System.Collections.Generic;
using AI;
using AI.BossPattern;
using BehaviourTree;
using Player;
using UnityEngine;

public class ExecutePattern : Node
{
    public override NodeState Evaluate(Node root)
    {
        Pattern pattern = GetData<Pattern>("currentPattern");
        pattern.caster = Boss.Instance;
        Boss.OnTriggerAttack = pattern.TouchPlayer;
        if (pattern == null) return NodeState.Failure;
        pattern.Execute();
        return NodeState.Success;
    }
}
