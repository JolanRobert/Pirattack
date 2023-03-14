using System.Collections.Generic;
using BehaviourTree;
using UnityEngine;
using UnityEngine.AI;

public class TaskPatrol : Node
{
    public Vector3 GetNearestPoint(Vector3 pos)
    {
        NavMeshHit hit;
        if (NavMesh.SamplePosition(pos, out hit, 4.0f, NavMesh.AllAreas))
            return hit.position;

        return Vector3.zero;
    }

    public override NodeState Evaluate(Node root)
    {
        List<Transform> patrolPoints = GetData<List<Transform>>("PatrolPoints");
        int index = (int)GetData("PatrolPointsIndex");
        Enemy enemy = GetData<Enemy>("caster");

        Vector3 target = GetNearestPoint(patrolPoints[index].position);
        if (Vector3.Distance(enemy.transform.position, target) < 1.5f)
        {
            index = (index == patrolPoints.Count - 1) ? 0 : index + 1;
            SetDataInBlackboard("PatrolPointsIndex", index);
        }

        enemy.Agent.SetDestination(target);
        return NodeState.Success;
    }
}