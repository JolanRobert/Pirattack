using System.Collections;
using System.Collections.Generic;
using AI;
using Player;
using TMPro;
using UnityEngine;
using Utils;

public class AITEST : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    [SerializeField] private Enemy simpleEnemy;
    [SerializeField] private EnemyShield enemyWithShield;
    [SerializeField] private Boss boss;

    [SerializeField] private TMP_Text textColorPlayer;
    
    [SerializeField] private PlayerController playerController;
        
    private PlayerData data => playerController.Data;

    private int index = 0;

    private void Start()
    {
        PrintPlayerColor();
    }

    private void ChangeArena(int index)
    {
        player.transform.position = new Vector3(0, 1, -35);
        simpleEnemy.gameObject.SetActive(false);
        enemyWithShield.gameObject.SetActive(false);
        boss.gameObject.SetActive(false);
        if (index == 0)
        {
            simpleEnemy.gameObject.SetActive(true);
            simpleEnemy.transform.position = new Vector3(0, 1, 25);
        }
        else if (index == 1)
        {
            enemyWithShield.gameObject.SetActive(true);
            enemyWithShield.transform.position = new Vector3(0, 1, 25);
        }
        else if (index == 2)
        {
            boss.gameObject.SetActive(true);
            boss.transform.position = new Vector3(0, 5, 0);
        }
    }

    private void Fire()
    {
        Bullet bullet = Pooler.Instance.Pop(Key.Bullet).GetComponent<Bullet>();
        bullet.transform.SetPositionAndRotation(player.transform.position, player.transform.rotation);
        bullet.Init(data, player);
    }

    private void PrintPlayerColor()
    {
        textColorPlayer.text = "Player Color : " + (player.Color == PlayerColor.Red ? "RED" : "BLUE");
    }


    private void SwapPlayerColor()
    {
        player.RequestSwitchColor();
        PrintPlayerColor();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) && index > 0)
        {
            ChangeArena(--index);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && index < 2)
        {
            ChangeArena(++index);
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Fire();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            SwapPlayerColor();
        }
    }
}