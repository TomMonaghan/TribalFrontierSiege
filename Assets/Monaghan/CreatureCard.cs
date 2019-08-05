using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureCard : CardBase
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
