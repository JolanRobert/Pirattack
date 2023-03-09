using Player;
using UnityEngine;

public class RespawnTrigger : InteractiveElement
{
    [SerializeField] private PlayerRespawn playerRespawn;

    private void Awake()
    {
        completionDuration = playerRespawn.RespawnDuration;
    }

    protected override void Complete()
    {
        playerRespawn.Respawn();
    }
}
