using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZapFX : MonoBehaviour
{
    public LineRenderer renderer;
    public float timer,delay;
    private void OnEnable()
    {
        renderer.widthMultiplier = 1;
        timer = delay;
    }

    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            renderer.widthMultiplier = Mathf.InverseLerp(0, delay, timer);
        }
    }
}
