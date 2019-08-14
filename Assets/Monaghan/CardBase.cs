using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CardBase : MonoBehaviour
{
    public int CurrentX { set; get;}
    public int CurrentY { set; get;}

    public bool isPlayerOne;

    public void SetPosition(int x, int y)
    {
        CurrentX = x;
        CurrentY = y;
    }
}
