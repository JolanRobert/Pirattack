using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Utils
{
    public class HealthBar : MonoBehaviour
    {
        public float RespawnAmount => respawnFill.fillAmount;

        [SerializeField] private Health health;
        [SerializeField] private GameObject healthGO;
        [SerializeField] private GameObject respawnGO;
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

        public void SetRespawnFill(float amount)
        {
            healthGO.SetActive(false);
            respawnGO.SetActive(true);
            
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
            
            healthGO.SetActive(true);
            respawnGO.SetActive(false);
        }
    }
}
