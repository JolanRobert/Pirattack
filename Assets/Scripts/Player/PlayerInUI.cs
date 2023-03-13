using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInUI : MonoBehaviour
{
    public void OnJoin(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        
        if (LobbyManager.Instance) LobbyManager.Instance.uiLobby.TryToJoinPlayer(context);
    }
}
