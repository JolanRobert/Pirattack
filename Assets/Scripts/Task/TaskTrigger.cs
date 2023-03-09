using MyBox;
using Player;
using UnityEngine;

namespace Task
{
    public class TaskTrigger : InteractiveElement
    {
        [SerializeField] private Task task;
        [SerializeField] private Renderer cubeRenderer;
        
        [SerializeField, ReadOnly] private PlayerColor requiredColor = PlayerColor.None;
        
        private void Awake()
        {
            completionDuration = task.CompletionDuration;
        }

        public override void Progress()
        {
            if (!task.IsValid()) return;
            
            base.Progress();
        }

        protected override void Complete()
        {
            task.Complete(this);
        }

        public void SetRequiredColor(PlayerColor color)
        {
            requiredColor = color;
            cubeRenderer.material.color = color == PlayerColor.Blue ? Color.blue : Color.red;
        }
        
        public bool Evaluate()
        {
            foreach (PlayerController player in players)
            {
                if (player.Color == requiredColor && player.IsInteracting) return true;
            }

            return false;
        }
    }
}
