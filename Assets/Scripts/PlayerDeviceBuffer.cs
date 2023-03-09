using UnityEngine;
using UnityEngine.InputSystem;

namespace DefaultNamespace
{
    [CreateAssetMenu(fileName = "New PlayerDeviceBuffer", menuName = "SO/PlayerDeviceBuffer")]
    public class PlayerDeviceBuffer : ScriptableObject
    {
        public InputDevice player1Device;
        public InputDevice player2Device;
    }
}