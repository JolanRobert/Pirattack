using DefaultNamespace;
using Player;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private PlayerDeviceBuffer devicesSO;
    [SerializeField] private Transform p1SpawnPoint;
    [SerializeField] private Transform p2SpawnPoint;

    private PlayerInput playerInput1, playerInput2;
    private void Start()
    {
        if (devicesSO.player1Device is null && Gamepad.all.Count > 0) devicesSO.player1Device = Gamepad.all[0];
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("Game"));
        
        var pim = PlayerInputManager.instance;
        playerInput1 = pim.JoinPlayer(playerIndex:0, controlScheme: "Gamepad", pairWithDevice: devicesSO.player1Device);
        playerInput2 = pim.JoinPlayer(playerIndex:1, controlScheme: "Gamepad", pairWithDevice: devicesSO.player2Device);
        
        var player1 = playerInput1.GetComponent<PlayerController>();
        var player2 = playerInput2.GetComponent<PlayerController>();
        
        player1.Init(p1SpawnPoint.position, PlayerColor.Blue);
        player2.Init(p2SpawnPoint.position, PlayerColor.Red);
        
        CameraManager.instance.InitializePlayer(playerInput1);
        CameraManager.instance.InitializePlayer(playerInput2);
    }

}
