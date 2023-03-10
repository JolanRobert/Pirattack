using System;
using System.Collections.Generic;
using System.Linq;
using MyBox;
using Player;
using UnityEngine;

namespace Task
{
    public class Task : MonoBehaviour
    {
        public Action<Task> OnComplete;
        
        public float CompletionDuration => completionDuration;
        
        [SerializeField] private float completionDuration;
        [SerializeField] private List<TaskTrigger> triggers;
        
        public void Init()
        {
            List<PlayerColor> colors = Enum.GetValues(typeof(PlayerColor)).Cast<PlayerColor>().ToList();
            colors.Remove(PlayerColor.None);
            colors.Shuffle();

            for (int i = 0; i < triggers.Count; i++)
            {
                triggers[i].SetRequiredColor(colors[i%colors.Count]);
            }
        }

        public bool IsValid()
        {
            foreach (TaskTrigger trigger in triggers)
            {
                if (!trigger.Evaluate()) return false;
            }

            return true;
        }

        public void Complete(TaskTrigger trigger)
        {
            if (trigger != triggers[0]) return;
            OnComplete.Invoke(this);
            Debug.Log("Task is complete!");
        }
    }
}