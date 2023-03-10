using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public PlayerController[] Players;
    
    
    private void Awake()
    {
        Instance = this;
    }
}
