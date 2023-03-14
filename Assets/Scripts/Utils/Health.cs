using System;
using System.Collections;
using MyBox;
using UnityEngine;

namespace Utils
{
    public class Health : MonoBehaviour
    {
        public int MaxHealth => maxHealth;
        public int CurrentHealth => currentHealth;
        
        public Action OnHealthGain;
        public Action OnHealthLose;
        public Action OnHealthReset;
        public Action OnDeath;

        [SerializeField, ReadOnly] private int maxHealth = -1;
        [SerializeField, ReadOnly] private int currentHealth = -1;
        [SerializeField, ReadOnly] private bool isImmortal;

        private int regenValue;
        private float regenTick;
        private Coroutine regenCR;

        public void Init(int maxHealth)
        {
            this.maxHealth = maxHealth;
            currentHealth = maxHealth;
        }

        public void StartPassiveRegeneration(int regenValue, float regenTick)
        {
            this.regenValue = regenValue;
            this.regenTick = regenTick;
            
            regenCR = StartCoroutine(PassiveRegeneration());
        }

        private IEnumerator PassiveRegeneration()
        {
            yield return new WaitForSeconds(regenTick);
            if (currentHealth > 0) GainHealth(regenValue);
            
            regenCR = StartCoroutine(PassiveRegeneration());
        }

        public void StopPassiveRegeneration()
        {
            StopCoroutine(regenCR);
        }

        public void GainHealth(int amount)
        {
            if (maxHealth == -1) Debug.LogError("Health has not been initialized!");
        
            currentHealth = Math.Clamp(currentHealth + amount, 0, maxHealth);
            OnHealthGain?.Invoke();
        }

        public void LoseHealth(int amount)
        {
            if (maxHealth == -1) Debug.LogError("Health has not been initialized!");
        
            currentHealth = Math.Clamp(currentHealth - amount, isImmortal ? 1 : 0, maxHealth);
            OnHealthLose?.Invoke();

            if (currentHealth == 0) OnDeath?.Invoke();
        }

        public void Reset() {
            SetHealth(maxHealth);
            OnHealthReset?.Invoke();
        }

        public void SetHealth(int amount)
        {
            amount = Math.Clamp(amount, 0, Mathf.Abs(maxHealth));
            currentHealth = amount;
        }

        [ContextMenu("SmoothKill")]
        public void SmoothKill()
        {
            LoseHealth(currentHealth);
        }
        
        [ContextMenu("InstantKill")]
        public void InstantKill()
        {
            currentHealth = 0;
            OnDeath?.Invoke();
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