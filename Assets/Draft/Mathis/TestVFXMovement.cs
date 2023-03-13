using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

public class TestVFXMovement : MonoBehaviour
{
    public Transform start,canon;
    public float timer, loop,speed,speedproj;
    public List<GameObject> proj;
    public GameObject prefab;
    public WeaponData weaponData;
    

    private void Start()
    {
        for (int i = 0; i < 20; i++)
        {
            proj.Add(Instantiate(prefab, Vector3.zero, Quaternion.identity));
        }
    }

    void Update()
    {
        
        transform.Rotate(0,speed*Time.deltaTime,0);
        if (timer > 0) timer -= Time.deltaTime;
        else
        {
            timer = loop;
            proj[0].SetActive(false);
            proj[0].transform.position = start.position;
            proj[0].SetActive(true);
            proj[0].transform.forward = canon.position -start.position;
            proj.Add(proj[0]);
            proj.RemoveAt(0);
        }

        for (int i = 0; i < proj.Count; i++)
        {
            proj[i].transform.position += proj[i].transform.forward * Time.deltaTime * speedproj;
        }
    }
}
