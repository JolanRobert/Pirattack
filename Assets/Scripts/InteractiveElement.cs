using System;
using System.Collections.Generic;
using DG.Tweening;
using Player;
using UnityEngine;
using UnityEngine.UI;

public class InteractiveElement : MonoBehaviour, IComparable<InteractiveElement>
{
    [SerializeField] private Image progressBar;

    protected HashSet<PlayerController> players = new HashSet<PlayerController>();
    protected float completionDuration = 1;

    private void OnEnable()
    {
        progressBar.fillAmount = 0;
    }
    
    private void OnDisable()
    {
        foreach (PlayerController player in players)
        {
            player.Interact.OnEndInteract -= Regress;
        }
        
        players.Clear();
    }

    public virtual void Progress()
    {
        Debug.Log("Progress");
        float newAmount = progressBar.fillAmount + Time.deltaTime / completionDuration;

        progressBar.DOKill();
        Tween tween = progressBar.DOFillAmount(newAmount, Time.deltaTime).SetEase(Ease.Linear);
        if (newAmount >= 1) tween.onComplete += Complete;
    }

    protected virtual void Regress()
    {
        Debug.Log("Regress");
        float duration = completionDuration*2 - (1-progressBar.fillAmount) * completionDuration*2;
        progressBar.DOKill();
        progressBar.DOFillAmount(0, duration).SetEase(Ease.Linear);
    }

    protected virtual void Complete() {}

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerController player))
        {
            players.Add(player);
            player.Interact.Subscribe(this);
            player.Interact.OnEndInteract += Regress;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out PlayerController player))
        {
            players.Remove(player);
            player.Interact.Unsubscribe(this);
            player.Interact.OnEndInteract -= Regress;
        }
    }

    public int CompareTo(InteractiveElement other)
    {
        return other is RespawnTrigger ? 1 : -1;
    }
}