using System.Collections;
using System.Collections.Generic;
using BehaviourTree;
using UnityEngine;
using UnityEngine.AI;
using Tree = BehaviourTree.Tree;

public class EnemyShieldBT : Tree
{
    [SerializeField] private EnemyShield enemy;
    [SerializeField] private NavMeshAgent agent;
    
    protected override Node InitTree()
    {
        origin = new Selector(new List<Node>
        {
            new InitEnemyBlackboard(),
            new CheckColorTarget(enemy),
            new TaskWaitForSeconds(),
            new Sequence(new List<Node>
            {
                new CanAttack(enemy.gameObject.transform),
                new Attack(enemy),
            }),
            new MoveToTarget(agent),
        });
        return origin;
    }
}
