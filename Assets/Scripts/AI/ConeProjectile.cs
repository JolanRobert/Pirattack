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
    private PlayerController target;

    IEnumerator Cone(EnemyShield caster)
    {
        while (currentSize < maxSize)
        {
            currentSize += delay;
            Vector3 direction = (target.transform.position - casterPosition).normalized;
            float angleBetween = Vector3.Angle(transform.forward, direction);
            if (angleBetween < angle / 2)
            {
                float distance = Vector3.Distance(casterPosition, target.transform.position);
                if (distance < currentSize)
                {
                    if (Physics.Raycast(casterPosition, direction, distance))
                    {
                        target.Collision.Damage(caster.Data.damage);
                        Debug.Log("Hit Player");
                    }
                }
            }

            //Debug
            Vector3 dir = (target.transform.position - casterPosition).normalized;
            Vector3 pos = casterPosition;
            Debug.DrawLine(pos, pos + dir * currentSize, Color.blue, 10f);
            //
            yield return new WaitForSeconds(delay);
        }
        Pooler.Instance.Depop(Key.Cone, gameObject);
    }

    public void Init(EnemyShield _caster, PlayerController _target)
    {
        maxSize = _caster.Data.maxSize;
        minSize = _caster.Data.minSize;
        delay = _caster.Data.speedPattern;
        angle = _caster.Data.angle;
        currentSize = minSize;
        casterPosition = _caster.transform.position;
        target = _target;
        StartCoroutine(Cone(_caster));
    }
}