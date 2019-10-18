using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTurn : MonoBehaviour
{
    private void OnMouseDown()
    {
        GameManager.Instance.EndTurn();
        if (GameManager.Instance.isPlayerOneTurn)
        {
            gameObject.GetComponent<Renderer>().material.color = Color.blue;
        }
        else
        {
            gameObject.GetComponent<Renderer>().material.color = Color.red;
        }
        
    }
}
