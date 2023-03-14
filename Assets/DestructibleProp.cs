using System;
using System.Collections;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;
using Utils;

public class DestructibleProp : MonoBehaviour,IDamageable
{
    [SerializeField] private MeshRenderer renderer;
    [SerializeField] private Material[] materials;
    [SerializeField] private bool iced;
    public void Damage(int damage)
    {
        transform.localScale = Vector3.one * 1.1f;
    }

    private void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale,Vector3.one, 5*Time.deltaTime);
    }
    
    public void SetIced(float duration)
    {
        if(iced) return;
        iced = true;
        renderer.material = materials[1];
        GameObject vfx = Pooler.Instance.Pop(Key.PerkIceVFX);
        vfx.transform.position = transform.position;
        Pooler.Instance.DelayedDepop(0.5f,Key.PerkIceVFX,vfx);
        StopIced(duration);
    }

    public async void StopIced(float duration)
    {
        await System.Threading.Tasks.Task.Delay(Mathf.FloorToInt(1000 * duration));
        renderer.material = materials[0];
        
        iced = false;
    }
}

