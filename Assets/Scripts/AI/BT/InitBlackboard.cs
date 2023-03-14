using System.Collections;
using System.Collections.Generic;
using BehaviourTree;
using UnityEngine;

public class InitEnemyBlackboard : Node
{
    private Enemy enemy;
    private Enemy enemyShield;

    public InitEnemyBlackboard(Enemy _enemy)
    {
        if (_enemy is EnemyShield)
            enemyShield = _enemy;
        else
            enemy = _enemy;
    }

    public override NodeState Evaluate(Node root)
    {
        var init = GetData("Init");
        if (init != null && (bool)init) return NodeState.Failure;
        SetDataInBlackboard("Init", true);
        SetDataInBlackboard("Target", null);
        SetDataInBlackboard("CanAttack", true);
        SetDataInBlackboard("WaitTime", 0f);
        SetDataInBlackboard("caster", enemyShield != null ? enemyShield : enemy);
        SetDataInBlackboard("PatrolPoints", enemyShield != null ? enemyShield.GetPatrolPoints() : enemy.GetPatrolPoints());
        SetDataInBlackboard("PatrolPointsIndex", 0);
        return NodeState.Success;
    }
}

public class InitBossBlackboard : Node
{
    private bool isInit = false;

    public override NodeState Evaluate(Node root)
    {
        if (isInit) return NodeState.Failure;
        isInit = true;
        SetDataInBlackboard("WaitTime", 0f);
        SetDataInBlackboard("WaitNode", null);
        return NodeState.Success;
    }
}