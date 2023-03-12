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
            progressBar.DOKill();
            progressBar.DOFillAmount(0, 1/lossAmountPerSec).SetEase(Ease.Linear);
        }
        
        private new void OnEnable()
        {
            base.OnEnable();

            timer = timeBeforeLosing;
            nextInput = Input.Any;
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
            float newAmount = progressBar.fillAmount + amountPerInput;

            progressBar.DOKill();
            Tween tween = progressBar.DOFillAmount(newAmount, Time.deltaTime).SetEase(Ease.Linear);
            if (newAmount >= 1) tween.onComplete += Complete;
        }

        private void DecreaseBar()
        {
            if (progressBar.fillAmount == 0) return;
            float newAmount = progressBar.fillAmount - Time.deltaTime*lossAmountPerSec;

            progressBar.DOKill();
            progressBar.DOFillAmount(newAmount, Time.deltaTime).SetEase(Ease.Linear);
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