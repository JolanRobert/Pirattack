using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleAnimation : MonoBehaviour
{
    [SerializeField] private float speed = 3f;

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.up * speed * Time.deltaTime;
        if (transform.position.y > 60f)
            Destroy(gameObject);
    }
}
