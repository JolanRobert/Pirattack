using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using MyBox;
using UnityEngine;

namespace Task
{
    public class TaskCauldron : ChaosTask
    {
        public CauldronInput NextInput => nextInput;
        
        [Separator("Task Cauldron")]
        [SerializeField, Range(0.01f,1)] private float amountPerInput;
        [SerializeField, Range(0.01f,1)] private float lossAmountPerSec;
        [SerializeField] private float timeBeforeLosing;
        
        [SerializeField] private GameObject imageA;
        [SerializeField] private GameObject imageX;
        [SerializeField] private GameObject imageY;
        
        [SerializeField, ReadOnly] private CauldronInput nextInput;
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
            nextInput = GetRandomInput(CauldronInput.A);
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            imageA.SetActive(false);
            imageX.SetActive(false);
            imageY.SetActive(false);
            if (UiIndicator.instance) UiIndicator.instance.RemoveObject(gameObject);
        }

        public void HandleInput(bool aInput, bool xInput, bool yInput)
        {
            List<bool> inputs = new List<bool> { aInput, xInput, yInput };
            CauldronInput input = ToCauldronInput(inputs);

            imageA.SetActive(nextInput == CauldronInput.A);
            imageX.SetActive(nextInput == CauldronInput.X);
            imageY.SetActive(nextInput == CauldronInput.Y);
            
            if (nextInput == input)
            {
                IncreaseBar();
                nextInput = GetRandomInput(nextInput);
                timer = timeBeforeLosing;
            }

            timer -= Time.deltaTime;
            if (timer <= 0) DecreaseBar();
        }

        private CauldronInput ToCauldronInput(List<bool> inputs)
        {
            for (int i = 0; i < inputs.Count; i++)
            {
                if (inputs[i]) return (CauldronInput)i;
            }

            return CauldronInput.None;
        }

        private CauldronInput GetRandomInput(CauldronInput previousInput)
        {
            List<CauldronInput> possibleInputs = Enum.GetValues(typeof(CauldronInput)).Cast<CauldronInput>().ToList();
            possibleInputs.Remove(previousInput);
            possibleInputs.Remove(CauldronInput.None);
            return possibleInputs.GetRandom();
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
        
        public enum CauldronInput
        {
            A, X, Y, None
        }
    }
}
