using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

public abstract class Pattern : ScriptableObject
{
    public Boss caster;

    public abstract float GetDelay();
    
    public abstract void TouchPlayer(PlayerController player); // Called when the boss touch the player
    
    public abstract void EndTrigger(GameObject obj); // Called when the boss stop touching the player

    public abstract void Execute();
}