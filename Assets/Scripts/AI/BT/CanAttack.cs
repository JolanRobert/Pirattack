using System.Collections;
using System.Collections.Generic;
using BehaviourTree;
using UnityEngine;

public class CanAttack : Node
{
    private Transform transform;
    
    public CanAttack(Transform _transform)
    {
        transform = _transform;
    }

    public override NodeState Evaluate(Node root)
    {
        bool canAttack = (bool)GetData("CanAttack");
        Player2 target = (Player2)GetData("Target");
        if (!target)
        {
           Player2[] players = MyGameManager.Instance.Players;
           target = (Vector3.Distance(players[0].transform.position, transform.position) <
                     Vector3.Distance(players[1].transform.position, transform.position)) ? 
               players[0] : players[1];
           SetDataInBlackboard("Target", target);
           SetDataInBlackboard("TargetPos", target);
        }
        if (canAttack && Vector3.Distance(target.transform.position, transform.position) <= 5)
        {
            Debug.Log("Can attack");
            return NodeState.Success;
        }
        return NodeState.Failure;
    }
}
