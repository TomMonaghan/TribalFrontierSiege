using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBase : MonoBehaviour
{
    public string cardName;
    public string cardDescription;
    public string cardType;
    public int goldCost;
    public int techCost;
    
    
    
        

        public void PrintBase()
        {
            Debug.Log("Card Name: " + cardName);
            Debug.Log("Card Description: " + cardDescription);
            Debug.Log("Card Type: " + cardType);
            Debug.Log("Gold Cost: " + goldCost);
            Debug.Log("Tech Cost: " + techCost);
            
        
        }

    
}
