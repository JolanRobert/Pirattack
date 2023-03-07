using System.Collections;
using System.Collections.Generic;
using BehaviourTree;
using UnityEngine;
using Tree = BehaviourTree.Tree;

public class BossBT : Tree
{
    protected override Node InitTree()
    {
        origin = new Selector(new List<Node>
        {
            new InitBossBlackboard(),
            new TaskWaitForSeconds(),
            new Sequence(new List<Node>
            {

            }),
        });
        return origin;
    }
}
