using System.Linq;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Utils
{
    public class DeviceManager
    {
        public static (bool, InputDevice, InputDevice) CheckAndTryToFixDevices(InputDevice p1Device, InputDevice p2Device)
        {
            if (p1Device is null || p2Device is null)
            {
                if (p1Device is null)
                {
                    foreach (var gamepad in Gamepad.all.Where(gamepad => !p2Device.Equals(gamepad)))
                    {
                        p1Device = gamepad;
                    }
                }

                if (p1Device is null) return p2Device is null ? (false, null, null) : (false, null, p2Device);
            
                if (p2Device is null)
                {
                    foreach (var gamepad in Gamepad.all.Where(gamepad => !p1Device.Equals(gamepad)))
                    {
                        p2Device = gamepad;
                    }
                }
            
                if (p2Device is null) return (false, null, null);
            }
            
            return (true, p1Device, p2Device);
        }
    }
}