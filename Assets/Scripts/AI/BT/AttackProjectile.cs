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

        ConeProjectile cone =  Pooler.Instance.Pop(Key.Cone).GetComponent<ConeProjectile>();
        cone.transform.SetPositionAndRotation(enemyShield.transform.position, enemyShield.transform.rotation);
        cone.Init(enemyShield, target);

        SetDataInBlackboard("CanAttack", false);
        SetDataInBlackboard("WaitTime", enemyShield.Data.maxSize - enemyShield.Data.minSize); 
        return NodeState.Success;
    }
}