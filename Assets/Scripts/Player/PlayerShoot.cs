using System.Collections;
using UnityEngine;
using Utils;

namespace Player
{
    public class PlayerShoot : MonoBehaviour
    {
        [SerializeField] private PlayerController playerController;
        [SerializeField] private Transform firePoint;

        private bool canShoot = true;
        private WeaponData weapon => playerController.Stats.Weapon;
        
        public void Shoot()
        {
            if (!canShoot) return;
            
            Bullet bullet = Pooler.Instance.Pop(Key.Bullet).GetComponent<Bullet>();
            bullet.transform.SetPositionAndRotation(firePoint.position, firePoint.rotation);
            bullet.Init(playerController, weapon);
            StartCoroutine(ShootCooldown());
            
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