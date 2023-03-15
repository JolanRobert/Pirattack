using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInUI : MonoBehaviour
{
    public void OnJoin(InputAction.CallbackContext context)
    {
        if (context.action.WasPressedThisFrame()) if (LobbyManager.Instance) LobbyManager.Instance.uiLobby.TryToJoinPlayer(context);
    }
}