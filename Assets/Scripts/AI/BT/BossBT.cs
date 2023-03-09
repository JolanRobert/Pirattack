using System;
using System.Collections;
using System.Collections.Generic;
using AI;
using AI.BossPattern;
using AI.BT;
using BehaviourTree;
using UnityEngine;
using UnityEngine.Serialization;
using Tree = BehaviourTree.Tree;

[Serializable]
public class BossBT : Tree
{
    [SerializeField] private Pattern[] allPatterns;
    [SerializeField] private Boss boss;

    protected override Node InitTree()
    {
        origin = new Selector(
            new InitBossBlackboard(),
            new InitPatternBoss(allPatterns, boss),
            new TaskWaitForSeconds(),
            new Sequence(
                new ChoosePattern(allPatterns),
                new ExecutePattern()
            ));
        return origin;
    }
}