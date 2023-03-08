using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;
using Utils;

public class ConeProjectile : MonoBehaviour
{
    [SerializeField] private GameObject FXCone;

    private float maxSize = 10;
    private float minSize = 0;
    private float currentSize = 0;
    private float delay = 1;
    private float angle = 45;
    private Vector3 casterPosition;

    IEnumerator Cone(EnemyShield caster)
    {
        PlayerController[] players = MyGameManager.Instance.Players;
        while (currentSize < maxSize)
        {
            currentSize += delay;
            for (int i = 0; i < players.Length; i++)
            {
                Vector3 direction = (players[i].transform.position - casterPosition).normalized;
                float angleBetween = Vector3.Angle(transform.forward, direction);
                if (angleBetween < angle / 2)
                {
                    float distance = Vector3.Distance(casterPosition, players[i].transform.position);
                    if (distance < currentSize)
                    {
                        if (Physics.Raycast(casterPosition, direction, distance))
                        {
                            players[i].Collision.Damage(caster.Data.damage);
                            Debug.Log("Hit Player");
                        }
                    }
                }
            }

            //Debug
            Vector3 dir = (players[0].transform.position - casterPosition).normalized;
            Vector3 pos = casterPosition;
            Debug.DrawLine(pos, pos + dir * currentSize, Color.blue, 10f);
            //
            yield return new WaitForSeconds(delay);
        }

        Pooler.Instance.Depop(Key.Cone, gameObject);
    }

    public void Init(EnemyShield _caster)
    {
        maxSize = _caster.Data.maxSize;
        minSize = _caster.Data.minSize;
        delay = _caster.Data.speedPattern;
        angle = _caster.Data.angle;
        currentSize = minSize;
        casterPosition = _caster.transform.position;
        StartCoroutine(Cone(_caster));
    }
}