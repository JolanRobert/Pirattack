using System.Collections.Generic;
using System.Linq;
using MyBox;
using UI;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MenuManager : Singleton<MenuManager>
{
    public UI_MainMenu uiMainMenu;
    public List<PlayerInput> players = new();

    private void Awake()
    {
        InitializeSingleton(false);
    }

    private void Start()
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("Menu"));
        
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
