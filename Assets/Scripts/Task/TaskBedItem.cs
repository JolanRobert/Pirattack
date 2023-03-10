using DG.Tweening;
using MyBox;
using UnityEngine;

namespace Task
{
    public class TaskBedItem : ChaosTask
    {
        [Separator("Task Bed Item")]
        [SerializeField] private TaskBed taskBed;

        protected override void OnCancel()
        {
            progressBar.DOKill();
            progressBar.fillAmount = 0;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            
            UiIndicator.instance.RemoveObject(gameObject);
        }

        public void HandleInput(bool aInput)
        {
            if (aInput) IncreaseBar();
        }

        private void IncreaseBar()
        {
            float newAmount = progressBar.fillAmount + taskBed.AmountPerInput;

            progressBar.DOKill();
            Tween tween = progressBar.DOFillAmount(newAmount, Time.deltaTime).SetEase(Ease.Linear);
            if (newAmount >= 1) tween.onComplete += Complete;
        }

        private void Complete()
        {
            taskBed.Progress(this);
            gameObject.SetActive(false);
        }
    }
}
