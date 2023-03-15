using AI;
using BehaviourTree;
using Managers;
using Player;
    using UnityEngine;

public class CanAttack : Node
{
    private Transform transform;

    public CanAttack(Transform _transform)
    {
        transform = _transform;
    }

    public void SetTarget()
    {
        //PlayerController[] players = GameManager.Instance.GetPlayers();
        PlayerController[] players = PlayerManager.Players.ToArray();
        PlayerController target;
        if (Vector3.Distance(players[0].transform.position, transform.position) <
            Vector3.Distance(players[1].transform.position, transform.position) && !players[0].IsDown)
        {
            target = players[0];
        }
        else if (!players[1].IsDown)
            target = players[1];
        else
        {
            transform.gameObject.SetActive(false);
            return;
        }
        SetDataInBlackboard("Target", target);
    }

    public override NodeState Evaluate(Node root)
    {
        bool canAttack = (bool)GetData("CanAttack");
        PlayerController target = GetData<PlayerController>("Target");
        if (!target || target.IsDown)
        {
            SetTarget();
            target = GetData<PlayerController>("Target");
        }

        var enemyShield = GetData("caster");
        float distanceMin = 1;
        transform.LookAt(target.transform);
        if (enemyShield != null && enemyShield is EnemyShield)
            distanceMin = (enemyShield as EnemyShield).data.attackDistance;
        else
            distanceMin = (enemyShield as Enemy).Data.attackDistance;

        if (!canAttack || !(Vector3.Distance(target.transform.position, transform.position) <= distanceMin + 0.5f))
            return NodeState.Failure;

        if (!Physics.Raycast(transform.position, target.transform.position - transform.position, out var hit, 100))
            return NodeState.Failure;

        if (hit.collider.gameObject != target.gameObject) return NodeState.Failure;

        return NodeState.Success;
    }
}