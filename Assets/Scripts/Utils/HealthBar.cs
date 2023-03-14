using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Utils
{
    public class HealthBar : MonoBehaviour
    {
        public float RespawnAmount => respawnFill.fillAmount;

        [SerializeField] private Health health;
        [SerializeField] private Image healthFill;
        [SerializeField] private Image respawnFill;

        private void OnEnable()
        {
            health.OnHealthLose += DoHealthFill;
            health.OnHealthGain += DoHealthFill;
            health.OnHealthReset += ResetFills;
        }

        private void OnDisable()
        {
            health.OnHealthLose -= DoHealthFill;
            health.OnHealthGain -= DoHealthFill;
            health.OnHealthReset -= ResetFills;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space)) health.SmoothKill();
            if (Input.GetKeyDown(KeyCode.E)) health.LoseHealth(50);
        }

        public void SetRespawnFill(float amount)
        {
            respawnFill.DOKill();
            respawnFill.fillAmount = amount;
        }

        public Tween DoRespawnFill(float endValue, float duration)
        {
            respawnFill.DOKill();
            return respawnFill.DOFillAmount(endValue, duration);
        }

        private void DoHealthFill()
        {
            healthFill.DOKill();
            healthFill.DOFillAmount(health.GetRatio(), 0.2f);
        }

        private void ResetFills()
        {
            healthFill.DOKill();
            healthFill.fillAmount = health.GetRatio();
            
            respawnFill.DOKill();
            respawnFill.fillAmount = 0;
        }
    }
}
