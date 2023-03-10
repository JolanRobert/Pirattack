using DefaultNamespace;
using Player;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSpawner : MonoBehaviour
{
    public static PlayerController Player1;
    public static PlayerController Player2;
    
    [SerializeField] private PlayerDeviceBuffer devicesSO;
    [SerializeField] private Transform p1SpawnPoint;
    [SerializeField] private Transform p2SpawnPoint;
    
    private void Start()
    {
        PlayerInputManager pim = PlayerInputManager.instance;
        pim.JoinPlayer(playerIndex:0, controlScheme: "Gamepad", pairWithDevice: devicesSO.player1Device);
        pim.JoinPlayer(playerIndex:1, controlScheme: "Gamepad", pairWithDevice: devicesSO.player2Device);
    }

    private void OnPlayerJoined(PlayerInput player)
    {
        if (player.playerIndex == 0)
        {
            Player1 = player.GetComponent<PlayerController>();
            Player1.StartPosition = p1SpawnPoint.position;
            Player1.Color.InitColor(PlayerColor.Blue);
        }
        else
        {
            Player2 = player.GetComponent<PlayerController>();
            Player2.StartPosition = p2SpawnPoint.position;
            Player2.Color.InitColor(PlayerColor.Red);
        }
        
        CameraManager.instance.InitializePlayer(player);
    }
}
