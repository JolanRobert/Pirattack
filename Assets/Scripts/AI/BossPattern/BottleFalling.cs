using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class BottleFalling : MonoBehaviour
{

    [SerializeField] private Rigidbody rb;
    
    private GameObject FX;

    public void Init(float speed, GameObject _FX)
    {
        rb.velocity = Vector3.down * speed;
        FX = _FX;
    }

    public void EndOfLife()
    {
        Pooler.Instance.Depop(Key.FXBottle, FX);
    }
}