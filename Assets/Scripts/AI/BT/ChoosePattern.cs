using AI.BossPattern;
using BehaviourTree;
using UnityEngine;

namespace AI.BT
{
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
            
            // while (pattern == GetData<Pattern>("currentPattern"))
            //        pattern = patterns[Random.Range(0, patterns.Length)];
            
            SetDataInBlackboard("WaitTime", pattern.GetDelay() + pattern.caster.data.delayBetweenPattern);
            SetDataInBlackboard("currentPattern", pattern);
            return NodeState.Success;
        }
    }
}
