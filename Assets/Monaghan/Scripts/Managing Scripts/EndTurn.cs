using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTurn : MonoBehaviour
{
    public int turnNumber = 1;
    public delegate void ButtonPush();
    public static ButtonPush OnButtonPush;
    
    //end the turn and start the next players turn when you click the button to the side of the board
    private void OnMouseDown()
    {
        GameManager.instance.EndTurn();
        //if player one turn
        if (GameManager.instance.isPlayerOneTurn)
        {
            gameObject.GetComponent<Renderer>().material.color = Color.blue;
            //PlayerTwoDrawCard
            OnButtonPush?.Invoke();
            turnNumber++;
        }
        //if player two turn
        else
        {
            gameObject.GetComponent<Renderer>().material.color = Color.red;
            //PlayerOneDrawCard
            OnButtonPush?.Invoke();
            turnNumber++;
        }
        
    }
}
