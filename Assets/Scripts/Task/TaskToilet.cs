using DG.Tweening;
using MyBox;
using UnityEngine;
using UnityEngine.UI;

namespace Task
{
    public class TaskToilet : ChaosTask
    {
        public ToiletInput NextInput => nextInput;
        
        [Separator("Task Toilet")]
        [SerializeField, Range(0.01f,1)] private float amountPerInput;
        [SerializeField, Range(0.01f,1)] private float lossAmountPerSec;
        [SerializeField] private float timeBeforeLosing;

        [SerializeField] private SpriteRenderer imageLB;
        [SerializeField] private SpriteRenderer imageRB;

        private ToiletInput nextInput;
        private float timer;
        
        protected override void OnCancel()
        {
            base.OnCancel();

            notifs[0].DoProgressFill(0, 1 / lossAmountPerSec);
            notifs[1].DoProgressFill(0, 1 / lossAmountPerSec);
        }
        
        private new void OnEnable()
        {
            base.OnEnable();

            timer = timeBeforeLosing;
            nextInput = ToiletInput.Any;
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            imageLB.gameObject.SetActive(false);
            imageRB.gameObject.SetActive(false);
            if (UiIndicator.instance) UiIndicator.instance.RemoveObject(gameObject);
        }

        public void HandleInput(bool leftInput, bool rightInput)
        {
            if (nextInput == ToiletInput.Any) HandleToiletImages(true, true);
            else if (nextInput == ToiletInput.Left) HandleToiletImages(true, false);
            else if (nextInput == ToiletInput.Right) HandleToiletImages(false, true);
            
            if (leftInput)
            {
                if (nextInput == ToiletInput.Right) return;
                IncreaseBar();
                nextInput = ToiletInput.Right;
                timer = timeBeforeLosing;
            }
            
            else if (rightInput)
            {
                if (nextInput == ToiletInput.Left) return;
                IncreaseBar();
                nextInput = ToiletInput.Left;
                timer = timeBeforeLosing;
            }

            timer -= Time.deltaTime;
            if (timer <= 0) DecreaseBar();
        }
        
        private void IncreaseBar()
        {
            float newAmount = notifs[0].ProgressAmount + amountPerInput;
            notifs[0].DoProgressFill(newAmount, Time.deltaTime);
            Tween tween = notifs[1].DoProgressFill(newAmount, Time.deltaTime);
            if (newAmount >= 1) tween.onComplete += Complete;
        }

        private void DecreaseBar()
        {
            if (notifs[0].ProgressAmount == 0) return;
            
            float newAmount = notifs[0].ProgressAmount - Time.deltaTime * lossAmountPerSec;
            notifs[0].DoProgressFill(newAmount, Time.deltaTime);
            notifs[1].DoProgressFill(newAmount, Time.deltaTime);
        }
        
        private void Complete()
        {
            OnComplete.Invoke(this);
        }
        
        private void HandleToiletImages(bool leftInput, bool rightInput)
        {
            imageLB.gameObject.SetActive(true);
            imageLB.transform.localScale = leftInput ? new Vector3(1.2f, 1.2f, 1.2f) : Vector3.one;
            imageLB.color = leftInput ? Color.white : new Color(0.7f, 0.7f, 0.7f);
                    
            imageRB.gameObject.SetActive(true);
            imageRB.transform.localScale = rightInput ? new Vector3(1.2f, 1.2f, 1.2f) : Vector3.one;
            imageRB.color = rightInput ? Color.white : new Color(0.7f, 0.7f, 0.7f);
        }
        
        public enum ToiletInput
        {
            Left, Right, Any
        }
    }
}