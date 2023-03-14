using System;
using System.Collections;
using Player;
using Unity.VisualScripting;
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
            InitializeWeaponData();
        }

        private void InitializeWeaponData()
        {
            currentWeaponData = ScriptableObject.CreateInstance<WeaponData>();
            currentWeaponData.damage = weaponData.damage;
            currentWeaponData.nbBounce = weaponData.nbBounce;
            currentWeaponData.nbShock = weaponData.nbShock;
            currentWeaponData.nbSlow = weaponData.nbSlow;
            currentWeaponData.nbPierce = weaponData.nbPierce;
            currentWeaponData.fireRate = weaponData.fireRate;
            
            currentWeaponData.bulletSpeed = weaponData.bulletSpeed;
            currentWeaponData.bulletLifespan = weaponData.bulletLifespan;
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