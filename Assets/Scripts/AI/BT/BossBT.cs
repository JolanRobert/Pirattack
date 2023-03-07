using System;
using System.Collections;
using System.Collections.Generic;
using BehaviourTree;
using UnityEngine;
using Tree = BehaviourTree.Tree;

[Serializable]
public class BossBT : Tree
{
    [SerializeField] private Pattern[] AllPatterns;
    [SerializeField] private Boss boss;
    [SerializeField] private float delayBetweenPatterns = 3f;

    protected override Node InitTree()
    {
        origin = new Selector(new List<Node>
        {
            new InitBossBlackboard(),
            new InitPatternBoss(AllPatterns, boss),
            new TaskWaitForSeconds(),
            new Sequence(new List<Node>
            {
                new ChoosePattern(AllPatterns, delayBetweenPatterns),
                new ExecutePattern()
            }),
        });
        return origin;
    }
}