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
            base.OnCancel();
            
            taskInfos.SetProgressFill(0);
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
            float newAmount = ProgressAmount + taskBed.AmountPerInput;
            Tween tween = taskInfos.DoProgressFill(newAmount, Time.deltaTime);
            if (newAmount >= 1) tween.onComplete += Complete;
        }

        private void Complete()
        {
            taskBed.Progress(this);
            gameObject.SetActive(false);
        }
    }
}
