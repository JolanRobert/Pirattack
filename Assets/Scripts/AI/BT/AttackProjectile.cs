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
        StartWaveAttack(target);
        SetDataInBlackboard("CanAttack", false);
        SetDataInBlackboard("WaitTime", (enemyShield.data.maxSize - enemyShield.data.minSize) /  enemyShield.data.speedPattern + enemyShield.data.attackSpeed); 
        GetData<TaskWaitForSeconds>("WaitNode").FinalCountdown = () => SetDataInBlackboard("CanAttack", true);
        enemyShield.Animator.SetTrigger("Attack");
        return NodeState.Success;
    }

    public async void StartWaveAttack(PlayerController target)
    {
        await System.Threading.Tasks.Task.Delay(Mathf.FloorToInt(1000 * 0.6f));
        ConeProjectile cone =  Pooler.Instance.Pop(Pooler.Key.Wave).GetComponent<ConeProjectile>();
        cone.transform.SetPositionAndRotation(enemyShield.transform.position, enemyShield.transform.rotation);
        cone.InitWave(enemyShield, target);
        enemyShield.shootFX.Play(true);
    }
}