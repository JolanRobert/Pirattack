using System.Collections;
using System.Collections.Generic;
using BehaviourTree;
using UnityEngine;

public class ChoosePattern : Node
{
    private Pattern[] patterns;
    private float delayBetweenPatterns;
    
    public ChoosePattern(Pattern[] _patterns, float _delayBetweenPatterns)
    {
        patterns = _patterns;
        delayBetweenPatterns = _delayBetweenPatterns;
    }
    public override NodeState Evaluate(Node root) 
    {
        Pattern pattern = patterns[Random.Range(0, patterns.Length)];
        // A decomenter quand il y aura + de 1 pattern
         // while (pattern == GetData<Pattern>("currentPattern"))
         //        pattern = patterns[Random.Range(0, patterns.Length)];
        //
        SetDataInBlackboard("WaitTime", pattern.delay + delayBetweenPatterns);
        SetDataInBlackboard("currentPattern", pattern);
        return NodeState.Success;
    }
}
