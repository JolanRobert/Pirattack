using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;

public class TestPopBoss : MonoBehaviour
{

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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            PopBoss();
        }
    }
}
