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
        // A decomenter quand il y aura + de 1 pattern
         // while (pattern == GetData<Pattern>("currentPattern"))
         //        pattern = patterns[Random.Range(0, patterns.Length)];
        //
        SetDataInBlackboard("WaitTime", pattern.GetDelay() + pattern.caster.Data.delayBetWeenPattern);
        SetDataInBlackboard("currentPattern", pattern);
        return NodeState.Success;
    }
}
