using BehaviourTree;
using Player;
using UnityEngine;
using UnityEngine.AI;

namespace AI.BT
{
    public class MoveToTarget : Node
    {
        private NavMeshAgent agent;
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
            if (target == null)
            {
                agent.SetDestination(agent.transform.position);
                return NodeState.Failure;
            }

            Vector3 direction = (target.transform.position - agent.transform.position).normalized;
            Vector3 destination = target.gameObject.transform.position/* - direction * distance*/;
            destination.y = agent.transform.position.y;
            agent.SetDestination(destination);
            return NodeState.Success;
        }
    }
}