using BehaviourTree;
using Player;

public class AttackCaC : Node
{
    private Enemy caster;

    public override NodeState Evaluate(Node root)
    {
        PlayerController target = GetData<PlayerController>("Target");
        if (target == null) return NodeState.Failure;
        
        caster = GetData<Enemy>("caster");
        
        caster.Attack(target);
        SetDataInBlackboard("CanAttack", false);
        GetData<TaskWaitForSeconds>("WaitNode").FinalCountdown = () => SetDataInBlackboard("CanAttack", true);
        SetDataInBlackboard("WaitTime", caster.Data.ATKSpeed);
        caster.Animator.SetTrigger("Attack");
        return NodeState.Success;
    }
}
