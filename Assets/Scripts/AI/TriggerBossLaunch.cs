using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using Player;
using UnityEngine;

public class TriggerBossLaunch : MonoBehaviour
{
    List<PlayerController> players = new ();
    private void OnTriggerEnter(Collider other)
    {
        PlayerController player = other.GetComponent<PlayerController>();
        if (!player) return;
        if (players.Contains(player))
        {
            players.Remove(player);
            return;
        }
        players.Add(player);
        if (players.Count != 2) return;
        GameManager.Instance.OnLaunchingBoss?.Invoke();
        gameObject.SetActive(false);
    }
    
    
    private void OnEnable()
    {
        players.Clear();
    }
}
