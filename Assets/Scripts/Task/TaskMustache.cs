using System.Collections;
using DG.Tweening;
using MyBox;
using UnityEngine;

namespace Task
{
    public class TaskMustache : ChaosTask
    {
        [Separator("Task Mustache")]
        [SerializeField] private float angleCheckTimer;
        [SerializeField, Range(0f,180f)] private float minAnglePerCheck;
        [SerializeField, Range(0.01f,1)] private float gainAmountPer30over;
        [SerializeField, Range(0.01f,1)] private float lossAmountPerSec;

        private Vector2 leftInput, rightInput;
        private Vector2 lastLeftInput, lastRightInput;
        private bool isCheckingLeft, isCheckingRight;

        public float currentAngle;
        
        protected override void OnCancel()
        {
            progressBar.fillAmount = 0;
        }
 
        private new void OnEnable()
        {
            base.OnEnable();

            isCheckingLeft = false;
            isCheckingRight = false;

            currentAngle = 0;
        }
 
        public void HandleInput(Vector2 leftInput, Vector2 rightInput)
        {
            this.leftInput = leftInput;
            this.rightInput = rightInput;
            
            if (leftInput != lastLeftInput && !isCheckingLeft) StartCoroutine(LeftSpinDetection());
            if (rightInput != lastRightInput && !isCheckingRight) StartCoroutine(RightSpinDetection());

            currentAngle -= Time.deltaTime * angleCheckTimer * minAnglePerCheck;
            if (currentAngle <= 0) currentAngle = 0;
            if (!DOTween.IsTweening(progressBar)) DecreaseBar();

            Debug.Log(currentAngle);
        }
        
        private void IncreaseBar()
        {
            float angleDiff = currentAngle - minAnglePerCheck;
            float newAmount = progressBar.fillAmount + angleDiff / 30 * gainAmountPer30over;

            progressBar.DOKill();
            Tween tween = progressBar.DOFillAmount(newAmount, angleCheckTimer).SetEase(Ease.Linear);
            if (newAmount >= 1) tween.onComplete += Complete;
        }

        private void DecreaseBar()
        {
            if (progressBar.fillAmount == 0) return;
            float newAmount = progressBar.fillAmount - Time.deltaTime * lossAmountPerSec;

            progressBar.DOKill();
            progressBar.DOFillAmount(newAmount, Time.deltaTime).SetEase(Ease.Linear);
        }
        
        private void Complete()
        {
            OnComplete.Invoke(this);
            Debug.Log("Task is complete!");
        }

        private IEnumerator LeftSpinDetection()
        {
            isCheckingLeft = true;
            lastLeftInput = leftInput;

            yield return new WaitForSeconds(angleCheckTimer);

            currentAngle += Vector2.Angle(lastLeftInput, leftInput);
            isCheckingLeft = false;
            
            CheckAngleDiff();
        }
        
        private IEnumerator RightSpinDetection()
        {
            isCheckingRight = true;
            lastRightInput = rightInput;

            yield return new WaitForSeconds(angleCheckTimer);
            
            currentAngle += Vector2.Angle(lastRightInput, rightInput);
            isCheckingRight = false;
            
            CheckAngleDiff();
        }

        private void CheckAngleDiff()
        {
            if (currentAngle > minAnglePerCheck) IncreaseBar();
            currentAngle = 0;
        }
    }
}