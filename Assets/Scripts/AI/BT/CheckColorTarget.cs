using System.Collections;
using System.Collections.Generic;
using BehaviourTree;
using Player;
using UnityEditor.SearchService;
using UnityEngine;

public class CheckColorTarget : Node
{
    private EnemyShield owner;

    private void SelectTarget()
    {
        PlayerController[] players = MyGameManager.Instance.Players;
        PlayerController target = (players[0].Color != owner.GetShieldColor()) ? players[0] : players[1];
        SetDataInBlackboard("Target", target);
        SetDataInBlackboard("WaitTime", owner.Data.delaySwitchTarget);
        TaskWaitForSeconds.FinalCountdown = null;
    }

    public override NodeState Evaluate(Node root)
    {
        owner = GetData<EnemyShield>("caster");
        if (owner.GetShieldColor() == PlayerColor.None) return NodeState.Success;

        PlayerController target = GetData<PlayerController>("Target");
        owner = GetData<EnemyShield>("caster");
        if (target != null && target.Color == owner.GetShieldColor())
            SelectTarget();
        else
            SelectTarget();
        return NodeState.Failure;
    }
}