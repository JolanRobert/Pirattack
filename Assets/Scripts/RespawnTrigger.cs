using System;
using System.Collections.Generic;
using Player;
using UnityEngine;

public class RespawnTrigger : MonoBehaviour
{
    [SerializeField] private PlayerRespawn playerRespawn;
    
    private HashSet<PlayerController> players = new HashSet<PlayerController>();
    private float timer;

    private void OnEnable()
    {
        timer = playerRespawn.RespawnDuration;
    }

    private void Update()
    {
        if (players.Count == 0) return;

        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            playerRespawn.Respawn();
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerController player))
        {
            players.Add(player);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out PlayerController player))
        {
            players.Remove(player);
        }
    }
}