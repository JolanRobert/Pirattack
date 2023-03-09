using System;
using MyBox;
using UI;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuManager : Singleton<MenuManager>
{
    public InputDevice player1Device, player2Device;
    public UI_MainMenu uiMainMenu;
    
    [SerializeField] private GameObject playerInUiGO;
    
    private void Awake()
    {
        InitializeSingleton(false);
    }

    private void Start()
    {
        foreach (var gamepad in Gamepad.all)
        {
            PlayerInput.Instantiate(playerInUiGO, controlScheme: "Gamepad", pairWithDevice: gamepad);
        }
    }
}
