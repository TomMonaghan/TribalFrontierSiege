using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreaturesCard : CardBase
{
    public int attack;
    public int health;


    public void PrintBase()
    {
        Debug.Log("Attack: " + attack);
        Debug.Log("Health: " + health);
        /*
        Debug.Log("Card Name: " + cardName);
        Debug.Log("Card Description: " + cardDescription);
        Debug.Log("Card Type: " + cardType);
        Debug.Log("Gold Cost: " + goldCost);
        Debug.Log("Tech Cost: " + techCost);
        */
    }
}
