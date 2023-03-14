using System;
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
        private WeaponData currentWeaponData;

        private void Start()
        {
            currentWeaponData = weaponData;
        }

        public void Shoot()
        {
            if (!canShoot) return;
            
            Bullet bullet = Pooler.Instance.Pop(Key.Bullet).GetComponent<Bullet>();
            bullet.transform.SetPositionAndRotation(firePoint.position, firePoint.rotation);
            bullet.Init(playerController, currentWeaponData);
            StartCoroutine(ShootCooldown());
            
            playerController.Animation.SetTrigger(PlayerAnimation.AnimTrigger.Attack);
        }

        private IEnumerator ShootCooldown()
        {
            canShoot = false;
            yield return new WaitForSeconds(1 / currentWeaponData.fireRate);
            canShoot = true;
        }
    }