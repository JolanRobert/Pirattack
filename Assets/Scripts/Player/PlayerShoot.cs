using System.Collections;
using UnityEngine;
using Utils;

namespace Player
{
    public class PlayerShoot : MonoBehaviour
    {
        [SerializeField] private PlayerController playerController;
        [SerializeField] private Transform firePoint;
        
        private PlayerData data => playerController.Data;
        [SerializeField] private WeaponData weapon;

        private bool canShoot = true;

        public void Shoot()
        {
            if (!canShoot) return;
            
            BulletBehavior bullet = Pooler.Instance.Pop(Key.Bullet).GetComponent<BulletBehavior>();
            bullet.transform.SetPositionAndRotation(firePoint.position, firePoint.rotation);
            bullet.Init(weapon, playerController);
            StartCoroutine(ShootCooldown());
        }

        private IEnumerator ShootCooldown()
        {
            canShoot = false;
            yield return new WaitForSeconds(1 / data.attackSpeed);
            canShoot = true;
        }
    }
}