using MyBox;
using Player;
using Task;
using UnityEngine;

namespace InteractiveTrigger
{
    public class TaskTrigger : InteractiveElement
    {
        public bool IsValid() => chaosTask.IsValid();
        
        [Separator("Task Trigger")]
        [SerializeField] protected ChaosTask chaosTask;
        [SerializeField] private Renderer cubeRenderer;
        
        [SerializeField, ReadOnly] private PlayerColor requiredColor = PlayerColor.None;
        
        public void SetRequiredColor(PlayerColor color)
        {
            requiredColor = color;
            cubeRenderer.material.color = color == PlayerColor.Blue ? Color.blue : Color.red;
        }
        
        public bool Evaluate()
        {
            foreach (PlayerController player in players)
            {
                if (requiredColor == PlayerColor.None || player.PColor == requiredColor) return true;
            }

            return false;
        }
    }
}
