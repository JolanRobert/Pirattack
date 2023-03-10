using System;
using System.Collections.Generic;
using MyBox;
using Player;
using UnityEngine;
using UnityEngine.UI;

public class InteractiveElement : MonoBehaviour, IComparable<InteractiveElement>
{
    public bool IsActive => isActive;

    [Separator("Interactive Element")]
    [SerializeField] protected Image progressBar;
    [SerializeField] protected Renderer cubeRenderer;

    protected HashSet<PlayerController> players = new HashSet<PlayerController>();
    protected bool isActive;

    protected void OnEnable()
    {
        isActive = true;
        if (progressBar) progressBar.fillAmount = 0;
    }
    
    protected void OnDisable()
    {
        isActive = false;
            
        foreach (PlayerController player in players)
        {
            player.Interact.Unsubscribe(this);
            player.Interact.OnEndInteract -= OnCancel;
        }
        
        players.Clear();
    }

    protected virtual void OnCancel() {}

    protected void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerController player))
        {
            players.Add(player);
            player.Interact.Subscribe(this);
            player.Interact.OnEndInteract += OnCancel;
        }
    }

    protected void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out PlayerController player))
        {
            players.Remove(player);
            player.Interact.Unsubscribe(this);
            player.Interact.OnEndInteract -= OnCancel;
        }
    }

    public int CompareTo(InteractiveElement other)
    {
        return other is RespawnTrigger ? 1 : -1;
    }
}