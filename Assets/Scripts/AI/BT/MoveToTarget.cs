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
            //first iteration
            PlayerController target = GetData<PlayerController>("Target");
            var agentPosition = agent.transform.position;
            var targetPosition = target.transform.position;
            if (target == null)
            {
                agent.SetDestination(Vector3.zero);
                return NodeState.Failure;
            }

            targetPosition.y = agentPosition.y;
            agent.SetDestination(targetPosition);

            //second iteration
            float angularVision = 60;
            int indexOfSeed = 6;
            float randomizeAngle = Random.Range(0, angularVision);
            var vec = Quaternion.Euler(0, angularVision * (indexOfSeed - 1) + randomizeAngle, 0) * Vector3.forward;
            agent.SetDestination(agent.destination + Vector3.forward * (agent.radius + agent.radius + 0.5f));


            return NodeState.Success;
        }
    }
}