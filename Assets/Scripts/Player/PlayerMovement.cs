using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private Rigidbody rb;
        [SerializeField] private PlayerController playerController;

        private PlayerData data => playerController.Data;
        
        private float _turnSmoothAngle;

        public void Move(Vector2 moveInput)
        {
            rb.velocity = new Vector3(moveInput.x, 0, moveInput.y) * data.moveSpeed;
        }

        public void Rotate(Vector2 rotateInput)
        {
            rb.angularVelocity = Vector3.zero;
            
            if (rotateInput.sqrMagnitude < 0.1f) return;

            var targetAngle = Mathf.Atan2(rotateInput.x, rotateInput.y) * Mathf.Rad2Deg;
            var angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothAngle, 0.1f);
            transform.rotation = Quaternion.Euler(0, angle, 0);
        }
    }
}