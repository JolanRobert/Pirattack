using DG.Tweening;
using Player;
using UnityEngine;
using UnityEngine.UI;

namespace Task
{
    public class TaskInfos : MonoBehaviour
    {
        public float GetProgressAmount() => progressFill.fillAmount;
        public float GetPinAmount() => pinFill.fillAmount;

        [SerializeField] private CanvasGroup progressGroup;
        [SerializeField] private Image progressFill;
        [SerializeField] private CanvasGroup pinGroup;
        [SerializeField] private Image pinImage;
        [SerializeField] private Image pinBackground;
        [SerializeField] private Image pinFill;

        public void SetProgressFill(float amount)
        {
            progressFill.DOKill();
            progressFill.fillAmount = amount;
        }
        
        public Tween DoProgressFill(float endValue, float duration)
        {
            progressFill.DOKill();
            return progressFill.DOFillAmount(endValue, duration).SetEase(Ease.Linear);
        }
        
        public void SetPinFill(float amount)
        {
            pinFill.DOKill();
            pinFill.fillAmount = amount;
        }
        
        public Tween DoPinFill(float endValue, float duration)
        {
            pinFill.DOKill();
            return pinFill.DOFillAmount(endValue, duration).SetEase(Ease.Linear);
        }

        public void PausePinFill(Image[] notifs)
        {
            pinFill.DOPause();
            notifs[0].DOPause();
            notifs[1].DOPause();
        }

        public void ResumePinFill(Image[] notifs)
        {
            pinFill.DOPlay();
            notifs[0].DOPlay();
            notifs[1].DOPlay();
        }

        public void SetPinColor(PlayerColor color)
        {
            if (color == PlayerColor.Blue)
            {
                pinImage.color = new Color(0, 190 / 255f, 1);
                pinBackground.color = new Color(155 / 255f, 1, 1);
                pinFill.color = new Color(0, 80 / 255f, 220 / 255f);
            }
            else if (color == PlayerColor.Red)
            {
                pinImage.color = new Color(1f, 0.25f, 0f);
                pinBackground.color = new Color(1f, 0.5f, 0.5f);
                pinFill.color = new Color(0.85f, 0.2f, 0f);
            }
        }

        public void Show(InfoGroup group)
        {
            if (group == InfoGroup.Progress)
            {
                progressGroup.DOFade(1, 0.2f);
                pinGroup.DOFade(0, 0.2f);
            }
            else
            {
                progressGroup.DOFade(0, 0.2f);
                pinGroup.DOFade(1, 0.2f);
            }
        }

        public enum InfoGroup
        {
            Progress, Pin
        }
    }
}
