using System;
using System.Collections;
using UI;
using UnityEngine;
using UnityEngine.UIElements;

public class UI_Countdown : MonoBehaviour
{
    private const string LB_1 = "1LB";
    private const string LB_2 = "2LB";
    private const string LB_3 = "3LB";
    
    private const string USS_DISPLAYED = "displayed";
    private const string USS_HIDDEN = "hidden";
    
    private Label lb1, lb2, lb3;
    private int currentAnim;

    private Action endAction;
    private bool started;
    public bool Started => started;

    public void Init(VisualElement root)
    {
        lb1 = root.Q<Label>(LB_1);
        lb2 = root.Q<Label>(LB_2);
        lb3 = root.Q<Label>(LB_3);
        
        lb1.RegisterCallback<TransitionEndEvent>(_ => NextAnim());
        lb2.RegisterCallback<TransitionEndEvent>(_ => NextAnim());
        lb3.RegisterCallback<TransitionEndEvent>(_ => NextAnim());
    }

    public void StartCountdown(Action toDoAtEnd)
    {
        started = true;
        currentAnim = 0;
        
        NextAnim();
        
        endAction -= toDoAtEnd;
        endAction += toDoAtEnd;
    }
    
    public void StopCountdown()
    {
        started = false;
        currentAnim = 0;
    }

    private void NextAnim()
    {
        if (!started) return;
        
        var isEven = currentAnim % 2 == 0;
        switch (currentAnim)
        {
            case <2:
                ChangeUSS(lb3, isEven ? USS_DISPLAYED : USS_HIDDEN, isEven ? USS_HIDDEN : USS_DISPLAYED);
                break;
            case <4:
                ChangeUSS(lb2, isEven ? USS_DISPLAYED : USS_HIDDEN, isEven ? USS_HIDDEN : USS_DISPLAYED);
                break;
            case <6:
                ChangeUSS(lb1, isEven ? USS_DISPLAYED : USS_HIDDEN, isEven ? USS_HIDDEN : USS_DISPLAYED);
                break;
            case 6:
                endAction?.Invoke();
                break;
        }
        currentAnim++;
    }

    private void ChangeUSS(VisualElement ve, string ussToAdd, string ussToRemove)
    {
        ve.RemoveFromClassList(ussToRemove);
        ve.AddToClassList(ussToAdd);
    }
}
