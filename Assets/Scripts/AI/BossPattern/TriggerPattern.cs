using AI;
using Player;
using UnityEngine;

public class TriggerPattern : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        PlayerController player = other.GetComponent<PlayerController>();
        if (player != null)
        {
            Boss.OnTriggerAttack?.Invoke(player);
        }
        Boss.Instance.currentPattern.EndTrigger(gameObject);
    }
}