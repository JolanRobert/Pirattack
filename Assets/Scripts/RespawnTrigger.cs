using System.Collections.Generic;
using DG.Tweening;
using Player;
using UnityEngine;
using UnityEngine.UI;

public class RespawnTrigger : MonoBehaviour
{
    [SerializeField] private PlayerRespawn playerRespawn;
    [SerializeField] private Image progressBar;
    
    private HashSet<PlayerController> players = new HashSet<PlayerController>();
    
    private void OnDisable()
    {
        foreach (PlayerController player in players)
        {
            RemoveInteraction(player);
        }
            
        players.Clear();
    }
    
    public void IncreaseBar()
    {
        float duration = playerRespawn.RespawnDuration - progressBar.fillAmount * playerRespawn.RespawnDuration;
        progressBar.DOKill();
        Tween tween = progressBar.DOFillAmount(1, duration).SetEase(Ease.Linear);
        tween.onComplete += () => playerRespawn.Respawn();
    }

    public void DecreaseBar()
    {
        float duration = playerRespawn.RespawnDuration*2 - (1-progressBar.fillAmount) * playerRespawn.RespawnDuration*2;
        progressBar.DOKill();
        progressBar.DOFillAmount(0, duration).SetEase(Ease.Linear);
    }
    
    private void RemoveInteraction(PlayerController player)
    {
        player.Interact.OnBeginInteract -= IncreaseBar;
        player.Interact.OnEndInteract -= DecreaseBar;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerController player))
        {
            players.Add(player);
            RemoveInteraction(player);
            player.Interact.OnBeginInteract += IncreaseBar;
            player.Interact.OnEndInteract += DecreaseBar;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out PlayerController player))
        {
            players.Remove(player);
            RemoveInteraction(player);
        }
    }
}
