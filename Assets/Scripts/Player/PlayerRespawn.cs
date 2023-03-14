using UnityEngine;
using Utils;

namespace Player
{
    public class PlayerRespawn : MonoBehaviour
    {
        public bool IsDown => isDown;
        
        [SerializeField] private PlayerController playerController;
        [SerializeField] private Health health;
        [SerializeField] private GameObject respawnTrigger;
        
        private PlayerData data => playerController.Data;

        private bool isDown;

        private void OnEnable()
        {
            health.OnDeath += Die;
        }

        private void OnDisable()
        {
            health.OnDeath -= Die;
        }

        private void Start()
        {
            health.Init(data.maxHealth);
            health.StartPassiveRegeneration(data.regenValue, data.regenTick);
        }

        private void Die()
        {
            respawnTrigger.SetActive(true);
            isDown = true;
            
            Debug.Log("Dead");
        }

        public void Respawn()
        {
            respawnTrigger.SetActive(false);
            isDown = false;
            health.Reset();
            
            Debug.Log("Respawn!");
        }
    }
}