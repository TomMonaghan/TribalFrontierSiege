using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrawCardPlayerOne : MonoBehaviour
{
    
    int drawnAmount = 0;

    public delegate void ButtonPush(int i);
    public static ButtonPush OnButtonPush;

    private void OnMouseDown()
    {
        OnButtonPush?.Invoke(1);
        

    }
}
