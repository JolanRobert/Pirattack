using MyBox;
using UI;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MenuManager : Singleton<MenuManager>
{
    public UI_MainMenu uiMainMenu;
    
    [SerializeField] private GameObject playerInUiGO;
    
    private void Awake()
    {
        InitializeSingleton(false);
    }

    private void Start()
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("Menu"));
        
        foreach (var gamepad in Gamepad.all)
        {
            PlayerInput.Instantiate(playerInUiGO, controlScheme: "Gamepad", pairWithDevice: gamepad);
        }
    }
}
