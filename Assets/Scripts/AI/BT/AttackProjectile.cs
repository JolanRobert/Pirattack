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
        SetDataInBlackboard("WaitTime", (enemyShield.Data.maxSize - enemyShield.Data.minSize) /  enemyShield.Data.speedPattern + enemyShield.Data.AttackSpeed); 
        TaskWaitForSeconds.FinalCountdown = () => SetDataInBlackboard("CanAttack", true);
        return NodeState.Success;
    }
}