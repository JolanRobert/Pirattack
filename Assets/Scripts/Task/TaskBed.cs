using DG.Tweening;
using MyBox;
using UnityEngine;

namespace Task
{
    public class TaskBed : ChaosTask
    {
        [Separator("Task Bed")]
        [SerializeField, Range(0.01f,1)] private float amountPerInput;
        
        protected override void OnCancel()
        {
            progressBar.fillAmount = 0;
        }
        
        public void HandleInput(bool aInput)
        {
            if (aInput) IncreaseBar();
        }

        private void IncreaseBar()
        {
            float newAmount = progressBar.fillAmount + amountPerInput;

            progressBar.DOKill();
            Tween tween = progressBar.DOFillAmount(newAmount, Time.deltaTime).SetEase(Ease.Linear);
            if (newAmount >= 1) tween.onComplete += Complete;
        }
        
        private void Complete()
        {
            OnComplete.Invoke(this);
            Debug.Log("Task is complete!");
        }
    }
}