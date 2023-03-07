using System.Collections;
using System.Collections.Generic;
using BehaviourTree;
using UnityEngine;

public class TaskWaitForSeconds : Node
{
    public override NodeState Evaluate(Node root)
    {
        float timer = (float)GetData("WaitTime");
        if (timer <= 0)
        {
            SetDataInBlackboard("CanAttack", true);
            return NodeState.Failure;
        }
        timer -= Time.deltaTime;
        SetDataInBlackboard("WaitTime", timer);
        return NodeState.Success;
    }
}
