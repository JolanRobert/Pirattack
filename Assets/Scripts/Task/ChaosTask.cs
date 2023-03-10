using System;
using System.Collections.Generic;
using System.Linq;
using InteractiveTrigger;
using MyBox;
using Player;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Task
{
    public class ChaosTask : MonoBehaviour
    {
        public Action<ChaosTask> OnComplete;

        [SerializeField] private TaskTrigger firstTrigger;
        [SerializeField] private TaskTrigger secondTrigger;
        
        public void Init()
        {
            if (secondTrigger != null) return;
            
            List<PlayerColor> colors = Enum.GetValues(typeof(PlayerColor)).Cast<PlayerColor>().ToList();
            colors.Remove(PlayerColor.None);
            
            PlayerColor rdmColor = colors[Random.Range(0, colors.Count)];
            firstTrigger.SetRequiredColor(rdmColor);
        }

        public bool IsValid()
        {
            if (!firstTrigger.Evaluate()) return false;
            return secondTrigger == null || secondTrigger.Evaluate();
        }

        public void Complete(TaskTrigger trigger)
        {
            if (trigger != firstTrigger) return;
            OnComplete.Invoke(this);
            Debug.Log("Task is complete!");
        }
    }
}