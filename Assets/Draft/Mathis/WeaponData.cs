using UnityEngine;

namespace Player
{
    [CreateAssetMenu(fileName = "New WeaponData", menuName = "Data/WeaponData")]
    public class WeaponData : ScriptableObject
    {

        [Header("Bullets")]
        public float bulletSpeed;


        [Header("Perks")] 
        public int damage;
        public int bounce;
        public float firerate;
        public int bulletNb;
        public int pierce;
        public int electric;
        public float slow;
    }
}