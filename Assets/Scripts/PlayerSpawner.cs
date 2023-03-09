using DefaultNamespace;
using Player;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private PlayerDeviceBuffer devicesSO;

    private void Awake()
    {
        PlayerInputManager pim = PlayerInputManager.instance;
        var p1= pim.JoinPlayer(playerIndex:0, controlScheme: "Gamepad", pairWithDevice: devicesSO.player1Device);
        var p2= pim.JoinPlayer(playerIndex:1, controlScheme: "Gamepad", pairWithDevice: devicesSO.player2Device);
        
        p1.gameObject.GetComponent<PlayerSwitchColor>().InitColor(PlayerColor.Blue);
        p2.gameObject.GetComponent<PlayerSwitchColor>().InitColor(PlayerColor.Red);
    }
}
