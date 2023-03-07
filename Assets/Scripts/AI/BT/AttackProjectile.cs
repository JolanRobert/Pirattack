using BehaviourTree;
using Player;

public class AttackProjectile : Node
{
    public override NodeState Evaluate(Node root)
    {
        PlayerController target = GetData<PlayerController>("Target");
        if (target == null) return NodeState.Failure;

//ADD CODE HERE to make the enemy shoot a projectile at the player

        SetDataInBlackboard("CanAttack", false);
        SetDataInBlackboard("WaitTime", 1f);
        return NodeState.Success;
    }
}