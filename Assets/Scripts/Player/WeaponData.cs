using UnityEngine;

namespace Player
{
    [CreateAssetMenu(fileName = "New WeaponData", menuName = "Data/WeaponData")]
    public class WeaponData : ScriptableObject
    {
        [Header("Bullets")]
        public float bulletSpeed;
        public float bulletLifespan;

        [Header("Perks")] 
        public int damage;
        public int nbBounce;
        public float fireRate;
        public int nbPierce;
        public int nbShock;
        public float nbSlow;
        //public int bulletNb;
    }
}