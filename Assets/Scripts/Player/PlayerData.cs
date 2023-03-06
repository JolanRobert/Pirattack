using UnityEngine;

namespace Player
{
    [CreateAssetMenu(fileName = "New PlayerData", menuName = "Data/PlayerData")]
    public class PlayerData : ScriptableObject
    {
        [Header("Movement")]
        public float moveSpeed;

        [Header("Shoot")]
        public float bulletSpeed;
        public float bulletLifespan;
        public float fireRate;
    }
}