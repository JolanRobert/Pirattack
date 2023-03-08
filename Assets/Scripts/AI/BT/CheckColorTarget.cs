using System.Collections;
using System.Collections.Generic;
using BehaviourTree;
using Player;
using UnityEditor.SearchService;
using UnityEngine;

public class CheckColorTarget : Node
{
    private EnemyShield owner;
    public CheckColorTarget(EnemyShield owner)
    {
        SetDataInBlackboard("caster", owner);
    }

    private void SelectTarget()
    {
        PlayerController[] players = MyGameManager.Instance.Players;
        PlayerController target = (players[0].Color != owner.GetShieldColor()) ? players[0] : players[1];
        SetDataInBlackboard("Target", target);
    }

    public override NodeState Evaluate(Node root)
    {
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