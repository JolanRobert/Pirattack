using System.Collections;
using System.Collections.Generic;
using BehaviourTree;
using UnityEngine;

public class ChoosePattern : Node
{
    private Pattern[] patterns;
    
    public ChoosePattern(Pattern[] _patterns)
    {
        patterns = _patterns;
    }
    public override NodeState Evaluate(Node root) 
    {
        Pattern pattern = patterns[Random.Range(0, patterns.Length)];
        SetDataInBlackboard("WaitTime", pattern.delay);
        SetDataInBlackboard("currentPattern", pattern);
        return NodeState.Success;
    }
}
