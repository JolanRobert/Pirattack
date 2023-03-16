using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;
using Random = UnityEngine.Random;

public class PerksLoot : MonoBehaviour
{
    public LootType perk;
    public GameObject fxLoot;
    public GameObject[] perksobj;
    public GameObject iconParent;
    public PlayerController[] controller = new PlayerController[2];
    public bool iconShown,taken;
    
    
    private void Awake()
    {
        foreach (var obj in perksobj)
        {
            obj.SetActive(false);
        }
        controller = new PlayerController[2];
        iconShown = taken = false;
        int rng = Random.Range(0, 6);
        perk = (LootType) rng;
        perksobj[rng].SetActive(true);
        iconParent.transform.localScale = Vector3.zero;
    }

    public void Update()
    {
        if (taken) return;
        if (!iconShown)
        {
            iconParent.transform.localScale = Vector3.Lerp(iconParent.transform.localScale,Vector3.zero, 5*Time.deltaTime);
        }
        else
        {
            iconParent.transform.localScale = Vector3.Lerp(iconParent.transform.localScale,Vector3.one, 5*Time.deltaTime);
        }
    }
    
    public void OnTriggerEnter(Collider other)
    {
        PlayerController player = other.GetComponent<PlayerController>();
        if (player)
        {
            if (controller[0] == null)
            {
                controller[0] = player;
                player.interraction += GetUpgradePlayer1;
            }
            else if (controller[1] == null)
            {
                controller[1] = player;
                player.interraction += GetUpgradePlayer2;
            }
            iconShown = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        PlayerController player = other.GetComponent<PlayerController>();
        if (player)
        {
            if (controller[0] == player)
            {
                controller[0] = null;
                player.interraction -= GetUpgradePlayer1;
            }
            else if (controller[1] == player)
            {
                controller[1] = null;
                player.interraction -= GetUpgradePlayer2;
            }

            if (controller[0] == null && controller[1] == null)
            {
                iconShown = false;
            }
        }
    }

    public void GetUpgradePlayer1()
    {
        taken = true;
        controller[0].Stats.AddStat(perk);
        controller[0].interraction -= GetUpgradePlayer1;
        if(controller[1]) controller[1].interraction -= GetUpgradePlayer2;
        Destroy(Instantiate(fxLoot, transform.position, Quaternion.Euler(0, 0, 0)),1);
        Destroy(gameObject);
    }
    
    public void GetUpgradePlayer2()
    {
        taken = true;
        controller[1].Stats.AddStat(perk);
        if(controller[0]) controller[0].interraction -= GetUpgradePlayer1;
        controller[1].interraction -= GetUpgradePlayer2;
        Destroy(Instantiate(fxLoot, transform.position, Quaternion.Euler(0, 0, 0)),1);
        Destroy(gameObject);
    }
}
