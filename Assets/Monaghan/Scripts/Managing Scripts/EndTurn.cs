using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTurn : MonoBehaviour
{
    public int turnNumber = 1;
    public delegate void ButtonPush();
    public static ButtonPush OnButtonPush;

    public delegate void ApplyDamage();
    public static ApplyDamage OnApplyDamage;
    
    //end the turn and start the next players turn when you click the button to the side of the board
    private void OnMouseDown()
    {
        GameManager.instance.EndTurn();
        //if player one turn
        if (GameManager.instance.isPlayerOneTurn)
        {
            gameObject.GetComponent<Renderer>().material.color = Color.blue;
        }
        //if player two turn
        else
        {
            gameObject.GetComponent<Renderer>().material.color = Color.red;
        }
        
        OnButtonPush?.Invoke();
        if (gameObject.activeInHierarchy)
        {
            StartCoroutine(SendApplyDamage());
        }
        
        ResourceManagerGold.instance.AddGold(1);
        turnNumber++;
    }

    IEnumerator SendApplyDamage()
    {
        yield return null;
        OnApplyDamage?.Invoke();

    }
    
}
