using UnityEngine;

namespace Utils
{
    public class LockTransform : MonoBehaviour
    {
        [SerializeField] private bool lockPosition;
        [SerializeField] private bool lockRotation;

        void Update()
        {
            if (lockPosition) transform.position = Vector3.zero;
            if (lockRotation) transform.rotation = Quaternion.identity;
        }
    }
}
