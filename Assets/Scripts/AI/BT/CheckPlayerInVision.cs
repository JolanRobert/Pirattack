using System.Collections;
using System.Collections.Generic;
using BehaviourTree;
using Player;
using UnityEngine;

public class CheckPlayerInVision : Node
{
    public override NodeState Evaluate(Node root)
    {
        PlayerController player = GetData<PlayerController>("Target");
        if (player) return NodeState.Failure;
        
        var enemyShield = GetData("caster");
        var transform = ((Enemy)GetData("caster")).transform;
        
        //PlayerController[] players = GameManager.Instance.GetPlayers();
        PlayerController[] players = PlayerManager.Players.ToArray();
        PlayerController target = null;
        
        float distanceplayer0 = Vector3.Distance(players[0].transform.position, transform.position);
        float distanceplayer1 = Vector3.Distance(players[1].transform.position, transform.position);
        float minViewRange = 1;
        
        if (enemyShield != null && enemyShield is EnemyShield)
            minViewRange = (enemyShield as EnemyShield).Data.viewRangeDetection;
        else
            minViewRange = (enemyShield as Enemy).Data.viewRangeDetection;
        
        if (distanceplayer0 < distanceplayer1 && distanceplayer0 < minViewRange)
            target = players[0];
        else if (distanceplayer1 < minViewRange)
            target = players[1];
        
        SetDataInBlackboard("Target", target);
        return target ? NodeState.Failure : NodeState.Success;
    }
}
