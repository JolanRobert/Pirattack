using System;
using System.Collections;
using UnityEngine;
using Utils;

namespace Player
{
    public class PlayerShoot : MonoBehaviour
    {
        public Action<WeaponData> OnShoot;
        
        [SerializeField] private PlayerController playerController;
        [SerializeField] private Transform firePoint;
        [SerializeField] private ParticleSystem muzzlePS;

        private bool canShoot = true;
        private WeaponData weapon => playerController.Stats.Weapon;
        
        public void Shoot()
        {
            if (!canShoot) return;
            
            Bullet bullet = Pooler.Instance.Pop(Pooler.Key.Bullet).GetComponent<Bullet>();
            bullet.transform.SetPositionAndRotation(firePoint.position, firePoint.rotation);
            bullet.Init(playerController, weapon);
            StartCoroutine(ShootCooldown());
            OnShoot?.Invoke(weapon);
            
            muzzlePS.Play();
            
            playerController.Animation.SetTrigger(PlayerAnimation.AnimTrigger.Attack);
        }

        private IEnumerator ShootCooldown()
        {
            canShoot = false;
            yield return new WaitForSeconds(1 / weapon.fireRate);
            canShoot = true;
        }

        
    }
}