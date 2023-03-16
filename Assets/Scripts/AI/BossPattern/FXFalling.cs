using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXFalling : MonoBehaviour
{
[SerializeField] private float speed = 2f;

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < .5f)
        {
            transform.position += Vector3.up * Time.deltaTime * speed;
        }
    }
}
