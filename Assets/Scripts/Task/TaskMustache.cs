using System.Collections;
using DG.Tweening;
using MyBox;
using UnityEngine;

namespace Task
{
    public class TaskMustache : ChaosTask
    {
        [Separator("Task Mustache")]
        [SerializeField, ReadOnly] private float angleCheckTimer;
        [SerializeField, Range(0f,360)] private float minAnglePerCheck;
        [SerializeField, Range(0.01f,1)] private float lossAmountPerSec;
        [SerializeField] private float speedFactor = 1;

        private Vector2 leftInput, rightInput;
        private Vector2 lastLeftInput, lastRightInput;
        private bool isChecking;

        private float currentAngle;
        private float timer;
        
        protected override void OnCancel()
        {
            progressBar.DOKill();
            progressBar.fillAmount = 0;
        }
 
        private new void OnEnable()
        {
            base.OnEnable();

            isChecking = false;
            currentAngle = 0;
            timer = 0.3f;
        }
 
        public void HandleInput(Vector2 leftInput, Vector2 rightInput)
        {
            this.leftInput = leftInput;
            this.rightInput = rightInput;

            if (!isChecking) StartCoroutine(SpinDetection());

            timer -= Time.deltaTime;
        }
        
        private void IncreaseBar()
        {
            float newAmount = progressBar.fillAmount + currentAngle / 360 * speedFactor;

            progressBar.DOKill();
            Tween tween = progressBar.DOFillAmount(newAmount, angleCheckTimer).SetEase(Ease.Linear);
            if (newAmount >= 1) tween.onComplete += Complete;
        }

        private void DecreaseBar()
        {
            if (progressBar.fillAmount == 0) return;
            float newAmount = progressBar.fillAmount - lossAmountPerSec*angleCheckTimer;

            progressBar.DOKill();
            progressBar.DOFillAmount(newAmount, angleCheckTimer).SetEase(Ease.Linear);
        }
        
        private void Complete()
        {
            OnComplete.Invoke(this);
            Debug.Log("Task is complete!");
        }

        private IEnumerator SpinDetection()
        {
            isChecking = true;

            lastLeftInput = leftInput;
            lastRightInput = rightInput;
            
            yield return new WaitForSeconds(angleCheckTimer);
            
            currentAngle += Vector2.Angle(lastLeftInput, leftInput);
            currentAngle += Vector2.Angle(lastRightInput, rightInput);
            currentAngle -= minAnglePerCheck;

            if (currentAngle >= 0)
            {
                IncreaseBar();
                timer = 0.3f;
            }
            else if (timer <= 0) DecreaseBar();

            currentAngle = 0;
            isChecking = false;
        }
    }
}