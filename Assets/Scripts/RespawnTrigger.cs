using DG.Tweening;
using MyBox;
using Player;
using UnityEngine;

namespace InteractiveTrigger
{
    public class RespawnTrigger : InteractiveElement
    {
        [Separator("Respawn Trigger")]
        [SerializeField] private PlayerRespawn playerRespawn;

        public void HandleInput(bool respawnInput)
        {
            if (respawnInput) IncreaseBar();
            else DecreaseBar();
        }
        
        private void IncreaseBar()
        {
            float newAmount = progressBar.fillAmount + Time.deltaTime / playerRespawn.RespawnDuration;

            progressBar.DOKill();
            Tween tween = progressBar.DOFillAmount(newAmount, Time.deltaTime).SetEase(Ease.Linear);
            if (newAmount >= 1) tween.onComplete += Complete;
        }

        private void DecreaseBar()
        {
            if (progressBar.fillAmount == 0) return;
            float newAmount = progressBar.fillAmount - Time.deltaTime / playerRespawn.RespawnDuration;

            progressBar.DOKill();
            progressBar.DOFillAmount(newAmount, Time.deltaTime).SetEase(Ease.Linear);
        }

        private void Complete()
        {
            playerRespawn.Respawn();
        }
    }
}
