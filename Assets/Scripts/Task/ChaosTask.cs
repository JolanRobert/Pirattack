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
    public class ChaosTask : InteractiveElement
    {
        public Action<ChaosTask> OnComplete;

        [SerializeField] private Renderer cubeRenderer;
        [SerializeField, ReadOnly] private PlayerColor requiredColor = PlayerColor.None;
        
        public void Init()
        {
            List<PlayerColor> colors = Enum.GetValues(typeof(PlayerColor)).Cast<PlayerColor>().ToList();
            colors.Remove(PlayerColor.None);
            
            PlayerColor rdmColor = colors[Random.Range(0, colors.Count)];
            SetRequiredColor(rdmColor);
        }
        
        private void SetRequiredColor(PlayerColor color)
        {
            requiredColor = color;
            cubeRenderer.material.color = color == PlayerColor.Blue ? Color.blue : Color.red;
        }
        
        public bool IsValid()
        {
            foreach (PlayerController player in players)
            {
                if (requiredColor == PlayerColor.None || player.PColor == requiredColor) return true;
            }

            return false;
        }

        protected void Complete()
        {
            OnComplete.Invoke(this);
            Debug.Log("Task is complete!");
        }
    }
}