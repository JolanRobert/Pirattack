using System;
using BehaviourTree;
using UnityEngine;

public class TaskWaitForSeconds : Node
{
    public static Action FinalCountdown;
    
    public override NodeState Evaluate(Node root)
    {
        float timer = (float)GetData("WaitTime");
        if (timer <= 0)
        {
            FinalCountdown?.Invoke();
            FinalCountdown = null;
            return NodeState.Failure;
        }
        timer -= Time.deltaTime;
        SetDataInBlackboard("WaitTime", timer);
        return NodeState.Success;
    }
}
