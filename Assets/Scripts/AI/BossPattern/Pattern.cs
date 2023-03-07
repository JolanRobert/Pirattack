using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pattern : MonoBehaviour
{
public float delay = 5f;
public Boss caster;
public Renderer FXRenderer;


public abstract void TouchPlayer(Player2 player);

public abstract void Execute();
}
