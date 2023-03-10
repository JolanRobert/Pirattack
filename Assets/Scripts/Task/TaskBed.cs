using System.Collections.Generic;
using MyBox;
using Player;
using UnityEngine;

namespace Task
{
    public class TaskBed : ChaosTask
    {
        public float AmountPerInput => amountPerInput;

        [Separator("Task Bed")]
        [SerializeField] private List<TaskBedItem> beds;
        [SerializeField, Range(0.01f,1)] private float amountPerInput;

        private List<TaskBedItem> bedsLeft;

        protected new void OnEnable()
        {
            base.OnEnable();

            bedsLeft = new List<TaskBedItem>(beds);
            foreach (TaskBedItem bed in beds)
            {
                bed.gameObject.SetActive(true);
            }
        }

        public override void Init()
        {
            foreach (TaskBedItem item in beds)
            {
                item.SetRequiredColor(GetRandomColor());
            }
        }

        public void Progress(TaskBedItem item)
        {
            bedsLeft.Remove(item);
            Debug.Log(bedsLeft.Count);
            if (bedsLeft.Count == 0) Complete();
        }

        private void Complete()
        {
            OnComplete.Invoke(this);
            Debug.Log("Task is complete!");
        }
    }
}