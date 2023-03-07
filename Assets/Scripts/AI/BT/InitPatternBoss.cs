using BehaviourTree;

public class InitPatternBoss : Node
{
    
    public InitPatternBoss(Pattern[] _patterns, Boss caster)
    {
        foreach (var pattern in _patterns)
        {
            pattern.caster = caster;
        }
    }
    
    public override NodeState Evaluate(Node root)
    {
        return NodeState.Failure;
    }
}
