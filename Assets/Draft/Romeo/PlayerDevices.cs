using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[Serializable]
public struct PlayerDevice
{
    public InputDevice device;
    public int playerId;
}

public class PlayerDevices : MonoBehaviour
{
    private PlayerDevice player1Device, player2Device;

    public void AddDevice(InputDevice device)
    {
        if (player1Device.IsUnityNull())
        {
            player1Device = new PlayerDevice
            {
                device = device,
                playerId = 0
            };
        }
        else if (player2Device.IsUnityNull())
        {
            player1Device = new PlayerDevice
            {
                device = device,
                playerId = 1
            };
        }
    }
}
