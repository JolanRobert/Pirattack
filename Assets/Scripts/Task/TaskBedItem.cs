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
            
            notifs[0].SetProgressFill(0);
            notifs[1].SetProgressFill(0);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            
            if (UiIndicator.instance) UiIndicator.instance.RemoveObject(gameObject);
        }

        public void HandleInput(bool aInput)
        {
            if (aInput) IncreaseBar();
        }

        private void IncreaseBar()
        {
            float newAmount = notifs[0].ProgressAmount + taskBed.AmountPerInput;
            notifs[0].DoProgressFill(newAmount, Time.deltaTime);
            Tween tween = notifs[1].DoProgressFill(newAmount, Time.deltaTime);
            if (newAmount >= 1) tween.onComplete += Complete;
        }

        protected override void Expire()
        {
            taskBed.Fail(this);
        }

        private void Complete()
        {
            taskBed.Progress(this);
            gameObject.SetActive(false);
        }
    }
}
