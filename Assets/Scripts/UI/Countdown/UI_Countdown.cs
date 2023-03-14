using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.UIElements;

public class UI_Countdown : MonoBehaviour
{
    [SerializeField] private UI_Lobby layout;

    private const string LB_1 = "1LB";
    private const string LB_2 = "2LB";
    private const string LB_3 = "3LB";
    
    private Label lb1, lb2, lb3;
    
    public void Init()
    {
        layout.Root.Q<Label>(LB_1);
        layout.Root.Q<Label>(LB_2);
        layout.Root.Q<Label>(LB_3);
    }
}
