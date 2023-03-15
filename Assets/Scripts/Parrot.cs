using Player;
using Task;
using UnityEngine;

public class Parrot : MonoBehaviour
{
    [SerializeField] private PlayerAnimation playerAnimation;

    private void OnEnable()
    {
        TaskManager.OnTaskAdded += NewTask;
    }

    private void OnDisable()
    {
        TaskManager.OnTaskAdded -= NewTask;
    }

    private void NewTask()
    {
        playerAnimation.SetTrigger(PlayerAnimation.AnimTrigger.TaskAdded);
    }
}
