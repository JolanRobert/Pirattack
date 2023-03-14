using AI;
using AI.BT;
using BehaviourTree;
using Player;
using UnityEngine;
using Utils;

public class AttackProjectile : Node
{
    private EnemyShield enemyShield;
    public override NodeState Evaluate(Node root)
    {
        PlayerController target = GetData<PlayerController>("Target");
        enemyShield = GetData<EnemyShield>("caster");
        if (target == null) return NodeState.Failure;

        ConeProjectile cone =  Pooler.Instance.Pop(Key.Wave).GetComponent<ConeProjectile>();
        cone.transform.SetPositionAndRotation(enemyShield.transform.position, enemyShield.transform.rotation);
        cone.InitWave(enemyShield, target);

        SetDataInBlackboard("CanAttack", false);
        SetDataInBlackboard("WaitTime", (enemyShield.data.maxSize - enemyShield.data.minSize) /  enemyShield.data.speedPattern + enemyShield.data.attackSpeed); 
        GetData<TaskWaitForSeconds>("WaitNode").FinalCountdown = () => SetDataInBlackboard("CanAttack", true);
        enemyShield.Animator.SetTrigger("Attack");
        return NodeState.Success;
    }
}