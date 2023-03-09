using UI;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInUI : MonoBehaviour
{
    public void OnJoin(InputAction.CallbackContext context)
    {
        MenuManager.Instance.uiMainMenu.TryToJoinPlayer(context);
    }
}
