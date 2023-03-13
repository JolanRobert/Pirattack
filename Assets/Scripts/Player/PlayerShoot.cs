using System.Collections;
using Player;
using UnityEngine;
using Utils;

    public class PlayerShoot : MonoBehaviour
    {
        [SerializeField] private PlayerController playerController;
        [SerializeField] private WeaponData weaponData;
        [SerializeField] private Transform firePoint;

        private bool canShoot = true;

        public void Shoot()
        {
            if (!canShoot) return;
            
            BulletBehavior bullet = Pooler.Instance.Pop(Key.Bullet).GetComponent<BulletBehavior>();
            bullet.transform.SetPositionAndRotation(firePoint.position, firePoint.rotation);
            bullet.Init(weaponData, playerController);
            StartCoroutine(ShootCooldown());
        }

        private IEnumerator ShootCooldown()
        {
            canShoot = false;
            yield return new WaitForSeconds(1 / weaponData.fireRate);
            canShoot = true;
        }
    }