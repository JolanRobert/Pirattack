using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    void Start()
    {
        CameraManager.instance.InitializePlayer(transform);
    }
}
