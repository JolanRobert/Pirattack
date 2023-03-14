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
        var caster = (Enemy)GetData("caster");
        var casterPos = caster.transform.position;
        
        //PlayerController[] players = GameManager.Instance.GetPlayers();
        if (PlayerManager.Players.Count == 0) return NodeState.Success;
        PlayerController[] players = PlayerManager.Players.ToArray();
        PlayerController target = null;
        
        float distancePlayer0 = Vector3.Distance(players[0].transform.position, casterPos);
        float distancePlayer1 = Vector3.Distance(players[1].transform.position, casterPos);
        float minViewRange = 10;
        
        if (enemyShield != null && enemyShield is EnemyShield)
            minViewRange = (enemyShield as EnemyShield).Data.viewRangeDetection;
        else
            minViewRange = (enemyShield as Enemy).Data.viewRangeDetection;
        
        if (distancePlayer0 < distancePlayer1 && distancePlayer0 < minViewRange)
            target = players[0];
        else if (distancePlayer1 < minViewRange)
            target = players[1];

        if (target)
            caster.OnPlayerOnVision();
        
        SetDataInBlackboard("Target", target);
        return target ? NodeState.Failure : NodeState.Success;
    }
}
