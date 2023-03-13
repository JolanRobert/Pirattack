using System.Collections.Generic;
using System.Linq;
using MyBox;
using UI;
using UnityEngine.InputSystem;

public class LobbyManager : Singleton<LobbyManager>
{
    public UI_MainMenu uiLobby;
    public List<PlayerInput> players = new();

    private void Awake()
    {
        InitializeSingleton(false);
    }

    public void InstantiatePlayers()
    {
        foreach (var gamepad in Gamepad.all)
        {
            AddPlayer(gamepad);
        }
    }

    public void AddPlayer(InputDevice device)
    {
        if (device is not Gamepad || IsDeviceAlreadyUsed(device)) return;
        players.Add(PlayerInputManager.instance.JoinPlayer(controlScheme: "Gamepad", pairWithDevice: device));
    }

    private bool IsDeviceAlreadyUsed(InputDevice device)
    {
        return players.Any(player => player.devices.ToList().Contains(device));
    }
}
