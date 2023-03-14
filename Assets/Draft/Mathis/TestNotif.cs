using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

public class TestNotif : MonoBehaviour
{
    public PlayerColor color;
    public UiIndicator uiIndicator;
    void Start()
    {
        uiIndicator.AddObject(gameObject,color);
    }

    
}
