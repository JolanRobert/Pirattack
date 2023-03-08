using UnityEngine;

namespace Utils
{
    [ExecuteAlways]
    public class LookAtCamera : MonoBehaviour
    {
        [SerializeField] private Mode mode;

        private Camera mainCam;

        private void Awake()
        {
            mainCam = Camera.main;
        }

        private void LateUpdate()
        {
            switch (mode)
            {
                case Mode.LookAt:
                    transform.LookAt(mainCam.transform);
                    break;
                case Mode.LookAtInverted:
                    Vector3 dirFromCamera = transform.position - mainCam.transform.position;
                    transform.LookAt(transform.position + dirFromCamera);
                    break;
                case Mode.CameraForward:
                    transform.forward = mainCam.transform.forward;
                    break;
                case Mode.CameraForwardInverted:
                    transform.forward = -mainCam.transform.forward;
                    break;
            }
        }
        
        private enum Mode
        {
            LookAt,
            LookAtInverted,
            CameraForward,
            CameraForwardInverted
        }
    }
}