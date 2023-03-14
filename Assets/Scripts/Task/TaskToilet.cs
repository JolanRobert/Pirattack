using System.Timers;
using DG.Tweening;
using MyBox;
using UnityEngine;

namespace Task
{
    public class TaskToilet : ChaosTask
    {
        [Separator("Task Toilet")]
        [SerializeField, Range(0.01f,1)] private float amountPerInput;
        [SerializeField, Range(0.01f,1)] private float lossAmountPerSec;
        [SerializeField] private float timeBeforeLosing;

        private Input nextInput;
        private float timer;
        
        protected override void OnCancel()
        {
            base.OnCancel();
            
            taskInfos.DoProgressFill(0, 1 / lossAmountPerSec);
        }
        
        private new void OnEnable()
        {
            base.OnEnable();

            timer = timeBeforeLosing;
            nextInput = Input.Any;
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            if (UiIndicator.instance) UiIndicator.instance.RemoveObject(gameObject);
        }

        public void HandleInput(bool leftInput, bool rightInput)
        {
            if (leftInput)
            {
                if (nextInput == Input.Right) return;
                IncreaseBar();
                nextInput = Input.Right;
                timer = timeBeforeLosing;
            }
            
            else if (rightInput)
            {
                if (nextInput == Input.Left) return;
                IncreaseBar();
                nextInput = Input.Left;
                timer = timeBeforeLosing;
            }

            timer -= Time.deltaTime;
            if (timer <= 0) DecreaseBar();
        }
        
        private void IncreaseBar()
        {
            float newAmount = ProgressAmount + amountPerInput;
            Tween tween = taskInfos.DoProgressFill(newAmount, Time.deltaTime);
            if (newAmount >= 1) tween.onComplete += Complete;
        }

        private void DecreaseBar()
        {
            if (ProgressAmount == 0) return;
            
            float newAmount = ProgressAmount - Time.deltaTime * lossAmountPerSec;
            taskInfos.DoProgressFill(newAmount, Time.deltaTime);
        }
        
        private void Complete()
        {
            OnComplete.Invoke(this);
            Debug.Log("Task is complete!");
        }
        
        private enum Input
        {
            Left, Right, Any
        }
    }
}