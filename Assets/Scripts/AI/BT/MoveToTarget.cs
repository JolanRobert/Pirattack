using BehaviourTree;
using Player;
using UnityEngine;
using UnityEngine.AI;

namespace AI.BT
{
    public class MoveToTarget : Node
    {
        private readonly NavMeshAgent agent;
        public MoveToTarget(NavMeshAgent _agent)
        {
            agent = _agent;
        }

        public override NodeState Evaluate(Node root)
        {
            //float distance = 0;
            /*var enemyShield = GetData("caster");
            if (enemyShield is EnemyShield shield)
                distance = shield.Data.AttackDistance;
            else
            {
                Enemy enemy = enemyShield as Enemy;
                if (enemy != null) distance = enemy.Data.AttackDistance;
            }*/
            PlayerController target = GetData<PlayerController>("Target");
            var agentPosition = agent.transform.position;
            var targetPosition = target.transform.position;
            if (target == null)
            {
                agent.SetDestination(agentPosition);
                return NodeState.Failure;
            }

            Vector3 direction = (targetPosition - agentPosition).normalized;
            Vector3 destination = targetPosition/* - direction * distance*/;
            destination.y = agentPosition.y;
            agent.SetDestination(destination);
            return NodeState.Success;
        }
    }
}