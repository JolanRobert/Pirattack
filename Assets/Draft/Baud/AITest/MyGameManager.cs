using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

public class MyGameManager : MonoBehaviour
{
    public static MyGameManager Instance;
    public PlayerController[] Players;
    
    
    private void Awake()
    {
        Instance = this;
    }
}
