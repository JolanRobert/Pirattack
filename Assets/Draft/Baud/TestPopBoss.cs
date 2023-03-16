using System;
using System.Collections;
using System.Collections.Generic;
using AI;
using Managers;
using Player;
using UnityEngine;

public class TestPopBoss : MonoBehaviour
{
Boss _boss;
    public void PopBoss()
    {
        GameManager.Instance.SuccessTask();
        GameManager.Instance.SuccessTask();
        GameManager.Instance.SuccessTask();
        GameManager.Instance.SuccessTask();
        GameManager.Instance.SuccessTask();
        GameManager.Instance.SuccessTask();
        GameManager.Instance.SuccessTask();
        GameManager.Instance.SuccessTask();
        GameManager.Instance.SuccessTask();
        GameManager.Instance.SuccessTask();
    }

    private void Start()
    {
        _boss = FindObjectOfType<Boss>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            PopBoss();
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            GameManager.Instance.SuccessTask();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            GameManager.Instance.FailTask();
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            GameManager.Instance.OnLaunchingBoss?.Invoke();
        }
        
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (!_boss)
            _boss = FindObjectOfType<Boss>();
            _boss.IsWasAttacked.Invoke(185, PlayerColor.Blue);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (!_boss)
            _boss = FindObjectOfType<Boss>();
            _boss.IsWasAttacked.Invoke(185, PlayerColor.Red);
        }
    }
}
