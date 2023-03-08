using BehaviourTree;
using Player;
using UnityEngine;
using UnityEngine.AI;

public class MoveToTarget : Node
{
    private NavMeshAgent agent;
    public MoveToTarget(NavMeshAgent _agent)
    {
        agent = _agent;
    }

    public override NodeState Evaluate(Node root)
    {
        float distance = 0;
        EnemyShield enemyShield = GetData<EnemyShield>("caster");
        if (enemyShield != null)
            distance = enemyShield.Data.AttackDistance;
        else
            distance = GetData<Enemy>("caster").Data.AttackDistance;
        PlayerController target = GetData<PlayerController>("Target");
        if (target == null)
        {
            agent.SetDestination(agent.transform.position);
            return NodeState.Failure;
        }

        Vector3 direction = (target.transform.position - agent.transform.position).normalized;
        agent.SetDestination(target.gameObject.transform.position - direction * distance);
        return NodeState.Success;
    }
}