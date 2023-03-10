using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayTEST : MonoBehaviour
{
[SerializeField] private Button buttonSuccessTask;
[SerializeField] private Button buttonFailTask;

private void Start()
{
    buttonSuccessTask.onClick.AddListener(() => { GameManager.Instance.SuccessTask(); });
    buttonFailTask.onClick.AddListener(() => { GameManager.Instance.FailTask(); });
}
}
