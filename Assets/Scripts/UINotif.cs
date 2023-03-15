using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UINotif : MonoBehaviour
{
    public float ProgressAmount => taskBarFill.fillAmount;
    
    public CanvasGroup canvasGroup;
    public Transform pinHead;
    
    [SerializeField] private Image pinFill;
    [SerializeField] private Image backBar;
    [SerializeField] private Image taskBar;
    [SerializeField] private Image taskBarFill;
    
    public void SetProgressBarActive(bool active)
    {
        taskBar.gameObject.SetActive(active);
        backBar.gameObject.SetActive(!active);
        pinHead.gameObject.SetActive(!active);
    }
    
    public void SetProgressFill(float amount)
    {
        taskBarFill.DOKill();
        taskBarFill.fillAmount = amount;
    }
    
    public Tween DoProgressFill(float endValue, float duration)
    {
        taskBarFill.DOKill();
        return taskBarFill.DOFillAmount(endValue, duration).SetEase(Ease.Linear);
    }
    
    public void SetAlpha(float amount)
    {
        canvasGroup.DOKill();
        canvasGroup.alpha = amount;
    }
    
    public Tween DoAlpha(float endValue, float duration)
    {
        canvasGroup.DOKill();
        return canvasGroup.DOFade(endValue, duration).SetEase(Ease.Linear);
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
    
    public void PausePinFill()
    {
        pinFill.DOPause();
    }

    public void ResumePinFill()
    {
        pinFill.DOPlay();
    }
}
