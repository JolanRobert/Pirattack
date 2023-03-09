using System.Collections;
using System.Collections.Generic;
using BehaviourTree;
using UnityEngine;
using UnityEngine.AI;
using Tree = BehaviourTree.Tree;

public class EnemyBT : Tree
{
    [SerializeField] private Enemy enemy;
    [SerializeField] private NavMeshAgent agent;
    
    protected override Node InitTree()
    {
        origin = new Selector(
            new InitEnemyBlackboard(enemy),
            new TaskWaitForSeconds(),
            new Sequence(
                new CanAttack(enemy.transform),
                new AttackCaC()
            ),
            new MoveToTarget(agent)
        );
        return origin;
    }
}