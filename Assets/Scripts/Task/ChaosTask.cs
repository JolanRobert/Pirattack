using System;
using System.Collections.Generic;
using System.Linq;
using MyBox;
using Player;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Task
{
    public class ChaosTask : InteractiveElement
    {
        public Action<ChaosTask> OnComplete;

        [SerializeField, ReadOnly] private PlayerColor requiredColor = PlayerColor.None;
        
        public virtual void Init()
        {
            SetRequiredColor(GetRandomColor());
        }

        public void SetRequiredColor(PlayerColor color)
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
        
        protected PlayerColor GetRandomColor()
        {
            List<PlayerColor> colors = Enum.GetValues(typeof(PlayerColor)).Cast<PlayerColor>().ToList();
            colors.Remove(PlayerColor.None);
            
            return colors[Random.Range(0, colors.Count)];
        }
    }
}