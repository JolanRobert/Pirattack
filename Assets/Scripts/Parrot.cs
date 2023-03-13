using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parrot : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private void OnEnable()
    {
        //INSERT HERE THE CODE TO SUBSCRIBE TO THE EVENT new Task
    }

    private void NewTask()
    {
        animator.SetTrigger("TaskAdded");
    }
}
