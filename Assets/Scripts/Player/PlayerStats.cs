using UnityEngine;

namespace Player
{
    public class PlayerStats : MonoBehaviour
    {
        public WeaponData Weapon => currentWeaponData;
        
        [SerializeField] private WeaponData baseWeaponData;
        private WeaponData currentWeaponData;
        
        private void Awake()
        {
            currentWeaponData = Instantiate(baseWeaponData);
        }
        
        public void AddStat(LootType type)
        {
            switch (type)
            {
                case LootType.Damage:
                    currentWeaponData.damage += (int)(baseWeaponData.damage * 0.4f);
                    break;
                case LootType.Bounce:
                    currentWeaponData.nbBounce++;
                    break;
                case LootType.Zap:
                    currentWeaponData.nbShock++;
                    break;
                case LootType.Slow:
                    currentWeaponData.nbSlow += baseWeaponData.nbSlow * 0.2f;
                    break;
                case LootType.Pierce:
                    currentWeaponData.nbPierce++;
                    break;
                case LootType.FireRate:
                    currentWeaponData.fireRate += baseWeaponData.fireRate * 0.2f;
                    break;
            }
        }
    }
}
