using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;
using Utils;

public class TestVFXMovement : MonoBehaviour
{
    public Transform start,canon;
    public float timer, loop,speed,speedproj;
    public List<GameObject> proj;
    public GameObject prefab;
    public WeaponData weaponData;
    public PlayerController controller;
    

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
            Bullet bullet = Pooler.Instance.Pop(Key.Bullet).GetComponent<Bullet>();
            bullet.transform.SetPositionAndRotation(canon.position, canon.rotation);
            bullet.Init(controller,weaponData);
        }

        for (int i = 0; i < proj.Count; i++)
        {
            proj[i].transform.position += proj[i].transform.forward * Time.deltaTime * speedproj;
        }
    }
}
