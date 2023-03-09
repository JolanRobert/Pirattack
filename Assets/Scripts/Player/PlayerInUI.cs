using UI;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInUI : MonoBehaviour
{
    public void OnJoin(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            MenuManager.Instance.uiMainMenu.TryToJoinPlayer(context);
        }
    }
}
