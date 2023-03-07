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
        [SerializeField] private List<TaskTrigger> triggers;

        private void OnEnable()
        {
            foreach (TaskTrigger trigger in triggers)
            {
                trigger.OnPlayerEnter += CheckCompletion;
            }
        }

        private void OnDisable()
        {
            foreach (TaskTrigger trigger in triggers)
            {
                trigger.OnPlayerEnter -= CheckCompletion;
            }
        }

        public void Init()
        {
            List<PlayerColor> colors = Enum.GetValues(typeof(PlayerColor)).Cast<PlayerColor>().ToList();
            colors.Shuffle();

            for (int i = 0; i < triggers.Count; i++)
            {
                triggers[i].SetRequiredColor(colors[i%colors.Count]);
            }
        }

        private void CheckCompletion()
        {
            foreach (TaskTrigger trigger in triggers)
            {
                if (!trigger.Evaluate()) return;
            }
            
            Debug.Log("Task completed");
        }
    }
}