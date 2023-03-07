using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

public class Pattern : ScriptableObject
{
    public float delay = 5f;
    public Boss caster;
    public Renderer FXRenderer;

    public virtual void TouchPlayer(PlayerController player) {} // Called when the boss touch the player

    public virtual void Execute(){}
}