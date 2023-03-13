using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

public class TriggerBossLaunch : MonoBehaviour
{
    List<PlayerController> players = new ();
    private void OnTriggerEnter(Collider other)
    {
        PlayerController player = other.GetComponent<PlayerController>();
        if (!player) return;
        if (players.Contains(player)) return;
        players.Add(player);
        if (players.Count != 2) return;
        GameManager.OnLaunchingBoss?.Invoke();
        gameObject.SetActive(false);
    }

    private void OnTriggerExit(Collider other)
    {
        PlayerController player = other.GetComponent<PlayerController>();
        if (player)
            players.Remove(player);
    }
}
