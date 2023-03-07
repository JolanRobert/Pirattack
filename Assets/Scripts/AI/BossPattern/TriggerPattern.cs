using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPattern : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Player2 player = other.GetComponent<Player2>();
        if (player != null)
        {
            Boss.OnTriggerAttack?.Invoke(player);
        }
    }
}