using System;
using System.Collections.Generic;
using AI;
using Interfaces;
using Player;
using UnityEngine;
using Utils;
using Random = UnityEngine.Random;

public class BulletTrigger : MonoBehaviour
{
    [SerializeField] private Bullet bullet;
    [SerializeField] private Collider bulletCollider;

    private PlayerController owner => bullet.Owner;

    private int damage;
    private int nbPierce;
    private int nbShock;
    private float nbSlow;
    
    public void Init(WeaponData data)
    {
        damage = data.damage;
        nbPierce = data.nbPierce;
        nbShock = data.nbShock;
        nbSlow = data.nbSlow;
    }
    
    private void SpawnImpactVFX(Vector3 position, Quaternion rotation)
    {
        GameObject vfx = VFXPooler.Instance.Pop(VFXPooler.Key.BulletImpactVFX);
        vfx.transform.SetPositionAndRotation(position, rotation);
        VFXPooler.Instance.DelayedDepop(0.3f, VFXPooler.Key.BulletImpactVFX, vfx);
    }
    
    //Layers Player/Enemy INCLUDED
    private void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger) return;

        //Hit entity
        if (other.TryGetComponent(out IDamageable entity))
        {
            DamageEntity(entity);
            SoundManager.Instance.PlayHitSound();
            SpawnImpactVFX(transform.position, transform.rotation);
           
            if (nbSlow > 0 && entity is Enemy enemy && owner) enemy.SetIced(1f,1-nbSlow);
            
            if (nbShock > 0) Shock(other.transform);
            
            if (nbPierce > 0)
            {
                nbPierce--;
                return;
            }
            
            bulletCollider.enabled = false;
            Pooler.Instance.Depop(Pooler.Key.Bullet, bullet.gameObject);
        }
    }

    private void DamageEntity(IDamageable entity)
    {
        if (entity is PlayerCollision && !owner) entity.Damage(damage);
        else if (entity is Enemy enemy && owner)enemy.IsWasAttacked.Invoke(damage, owner);
        else if (entity is DestructibleProp prop) prop.Damage(damage);
    }
    
    private void Shock(Transform originalTarget)
    {
        Transform currentTarget = originalTarget;
        
        List<GameObject> targetedObjects = new List<GameObject> { originalTarget.gameObject };

        for (int i = 0; i < nbShock; i++)
        {
            var targets = Physics.OverlapSphere(currentTarget.position, 1.2f);
            var targetables = new List<IDamageable>();
            var targetableObjects = new List<GameObject>();
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
                GameObject vfx = VFXPooler.Instance.Pop(VFXPooler.Key.PerkZapVFX);
                vfx.transform.position = new Vector3(currentTarget.position.x, transform.position.y, currentTarget.position.z);
                VFXPooler.Instance.DelayedDepop(0.6f,VFXPooler.Key.PerkZapVFX,vfx);
            }
            else break;
        }

        if (targetedObjects.Count > 0)
        {
            LineRenderer shockLineRd = VFXPooler.Instance.Pop(VFXPooler.Key.PerkZapLine).GetComponent<LineRenderer>();
            shockLineRd.positionCount = targetedObjects.Count;
            for (int i = 0; i < targetedObjects.Count; i++)
            {
                Vector3 pos = targetedObjects[i].transform.position;
                shockLineRd.SetPosition(i,new Vector3(pos.x,transform.position.y+0.5f,pos.z));
            }
            VFXPooler.Instance.DelayedDepop(0.1f,VFXPooler.Key.PerkZapLine,shockLineRd.gameObject);
        }
    }
}