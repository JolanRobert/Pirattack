using UnityEngine;

namespace Player
{
    public class PlayerAnimation : MonoBehaviour
    {
        [SerializeField] private Animator playerAnimator;
        [SerializeField] private Animator parrotAnimator;
        
        private static readonly int Velocity = Animator.StringToHash("Velocity");
        private static readonly int Attack = Animator.StringToHash("Attack");
        private static readonly int Switch = Animator.StringToHash("Switch");
        private static readonly int TaskAdded = Animator.StringToHash("TaskAdded");

        public void SetVelocity(float value)
        {
            playerAnimator.SetFloat(Velocity, value);
        }

        public void SetTrigger(AnimTrigger trigger)
        {
            switch (trigger)
            {
                case AnimTrigger.Attack:
                    playerAnimator.SetTrigger(Attack);
                    break;
                case AnimTrigger.Switch:
                    parrotAnimator.SetTrigger(Switch);
                    break;
                case AnimTrigger.TaskAdded:
                    parrotAnimator.SetTrigger(TaskAdded);
                    break;
            }
        }
        
        public enum AnimTrigger {
            Attack, Switch, TaskAdded
        }
    }
}
