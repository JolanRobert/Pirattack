using System;
using System.Collections.Generic;
using Interfaces;
using Player;
using UnityEngine;
using Utils;
using Random = UnityEngine.Random;

public class BulletTrigger : MonoBehaviour
{
    [SerializeField] private Bullet bullet;
    [SerializeField] private Collider collider;
    [SerializeField] private GameObject contactParticleSystem;
    [SerializeField] private GameObject zapParticleSystem;
    [SerializeField] private GameObject shockLine;

    private PlayerController owner => bullet.Owner;

    private float speed;
    private int damage;
    private int nbBounce;
    private int nbPierce;
    private int nbShock;
    private float nbSlow;
    
    public void Init(WeaponData data)
    {
        speed = data.bulletSpeed;
        damage = data.damage;
        nbBounce = data.nbBounce;
        nbPierce = data.nbPierce;
        nbShock = data.nbShock;
        nbSlow = data.nbSlow;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger) return;

        //Hit entity
        if (other.TryGetComponent(out IDamageable entity))
        {
            DamageEntity(entity);
            GameObject vfx = Pooler.Instance.Pop(Key.BulletImpactVFX);
            vfx.transform.position = transform.position;
            vfx.transform.rotation = transform.rotation;
            Pooler.Instance.DelayedDepop(0.3f,Key.BulletImpactVFX,vfx);
           
            if (nbSlow > 0 && entity is DestructibleProp prop && owner)
            {
                prop.SetIced(nbSlow);
            }
            
            if (nbShock > 0)
            {
                Shock(other.transform);
            }
            
            if (nbPierce > 0)
            {
                nbPierce--;
                return;
            }
            
            Debug.Log("trigger with "+other.name);
            collider.enabled = false;
            Pooler.Instance.Depop(Key.Bullet, bullet.gameObject);
        }
    }

    void DamageEntity(IDamageable entity)
    {
        if (entity is PlayerCollision && !owner) entity.Damage(damage);
        else if (entity is Enemy enemy && owner)enemy.IsWasAttacked.Invoke(damage, owner);
        else if (entity is DestructibleProp prop) prop.Damage(damage);
    }
    
    public void Shock(Transform originalTarget)
    {
        Transform currentTarget = originalTarget;
        
        List<GameObject> targetedObjects = new List<GameObject>(0);
        targetedObjects.Add(originalTarget.gameObject);
        List<IDamageable> targetables = null;
        List<GameObject> targetableObjects = null;
        Collider[] targets;
                    
        for (int i = 0; i < nbShock; i++)
        {
            targets = Physics.OverlapSphere(currentTarget.position, 1.2f);
            targetables = new List<IDamageable>(0);
            targetableObjects = new List<GameObject>(0);
            for (int j = 0; j < targets.Length; j++)
            {
                targets[j].TryGetComponent(out IDamageable entity);
                if (entity != null && !targetedObjects.Contains(targets[j].gameObject))
                {
                    targetables.Add(entity);
                    targetableObjects.Add(targets[j].gameObject);
                }
            }
            if (targetables.Count > 0)
            {
                int rng = Random.Range(0, targetables.Count);
                DamageEntity(targetables[rng]);
                targetedObjects.Add(targetableObjects[rng]);
                currentTarget = targetableObjects[rng].transform;
                GameObject vfx = Pooler.Instance.Pop(Key.PerkZapVFX);
                vfx.transform.position = new Vector3(currentTarget.position.x, transform.position.y, currentTarget.position.z);
                Pooler.Instance.DelayedDepop(0.6f,Key.PerkZapVFX,vfx);
            }
            else
            {
                break;
            }
        }

        if (targetedObjects.Count > 0)
        {
            LineRenderer shockLineRd = Pooler.Instance.Pop(Key.PerkZapLine).GetComponent<LineRenderer>();
            shockLineRd.positionCount = targetedObjects.Count;
            for (int i = 0; i < targetedObjects.Count; i++)
            {
                Vector3 pos = targetedObjects[i].transform.position;
                shockLineRd.SetPosition(i,new Vector3(pos.x,transform.position.y+0.5f,pos.z));
            }
            Pooler.Instance.DelayedDepop(0.1f,Key.PerkZapLine,shockLineRd.gameObject);
        }
    }
}
