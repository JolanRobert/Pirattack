using System;
using MyBox;
using UnityEngine;

namespace Utils
{
    public class Health : MonoBehaviour
    {
        public int MaxHealth => maxHealth;
        public int CurrentHealth => currentHealth;
        
        public Action onHealthGain;
        public Action onHealthLose;
        public Action onDeath;
        //public UnityEvent onHealthReset;

        [SerializeField, ReadOnly] private int maxHealth = -1;
        [SerializeField, ReadOnly] private int currentHealth = -1;
        [SerializeField, ReadOnly] private bool isImmortal;

        public void Init(int maxHealth)
        {
            this.maxHealth = maxHealth;
            currentHealth = maxHealth;
        }

        public void GainHealth(int amount)
        {
            if (maxHealth == -1) Debug.LogError("Health has not been initialized!");
        
            currentHealth = Math.Clamp(currentHealth + amount, 0, maxHealth);
            onHealthGain?.Invoke();
        }

        public void LoseHealth(int amount)
        {
            if (maxHealth == -1) Debug.LogError("Health has not been initialized!");
        
            currentHealth = Math.Clamp(currentHealth - amount, isImmortal ? 1 : 0, maxHealth);
            onHealthLose?.Invoke();

            if (currentHealth == 0) onDeath?.Invoke();
        }

        public void Reset() {
            SetHealth(maxHealth);
            //onHealthReset?.Invoke();
        }

        public void SetHealth(int amount)
        {
            amount = Math.Clamp(amount, 0, Mathf.Abs(maxHealth));
            currentHealth = amount;
        }

        public void Kill()
        {
            currentHealth = 0;
            onDeath?.Invoke();
        }

        public void FlipImmortality()
        {
            isImmortal = !isImmortal;
        }
    
        /// <summary>
        /// Return health value between 0 and 1
        /// </summary>
        /// <returns></returns>
        public float GetRatio()
        {
            return (float)currentHealth / maxHealth;
        }
    }
}