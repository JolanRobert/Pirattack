using Managers;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInUI : MonoBehaviour
{
    public void OnJoin(InputAction.CallbackContext context)
    {
        if (!context.action.WasPerformedThisFrame()) return;

        if (LobbyManager.Instance) LobbyManager.Instance.uiLobby.TryToJoinPlayer(context);
    }
}
