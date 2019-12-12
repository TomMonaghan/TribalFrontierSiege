using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResourceManagerGold : MonoBehaviour
{
    public static ResourceManagerGold instance;
    
    public int playerOneCurrentGold = 4;
    public int playerOneCurrentTechAmount = 1;
    public int playerOneCurrentArmySize = 1;
    public int playerTwoCurrentGold = 4;
    public int playerTwoCurrentTechAmount = 1;
    public int playerTwoCurrentArmySize = 1;
    
    public List < TextMeshPro > playerOneGoldTexts;
    public List < TextMeshPro > playerTwoGoldTexts;
    public List < TextMeshPro > playerOneTechTexts;
    public List < TextMeshPro > playerTwoTechTexts;
    public List < TextMeshPro > playerOneArmySizeTexts;
    public List < TextMeshPro > playerTwoArmySizeTexts;

    public int CurrentPlayerGold
    {
        get
        {
            if (GameManager.instance.isPlayerOneTurn)
            {
                return playerOneCurrentGold;
            }
            else
            {
                return playerTwoCurrentGold;
            }
        }
    }

    private void Start()
    { 
        instance = this;
        
        playerOneCurrentGold = playerOneCurrentGold + 1;
        playerOneCurrentTechAmount = 1;
        playerOneCurrentArmySize = 1;
        playerTwoCurrentGold = 4;
        playerTwoCurrentTechAmount = 1;
        playerTwoCurrentArmySize = 1;
        
        UpdateValues();

        
    }

    public void MinusGold(int howMuchGold)
    {
       
            if (GameManager.instance.isPlayerOneTurn)
            {
                playerOneCurrentGold -= howMuchGold;
            }
            else
            {
                playerTwoCurrentGold -= howMuchGold;

            }
            
            UpdateValues();
    }

    public void AddGold(int howMuchGold)
    {
        
            if (GameManager.instance.isPlayerOneTurn)
            {
                playerOneCurrentGold += howMuchGold;
            }
            else
            {
                playerTwoCurrentGold += howMuchGold;

            }

            UpdateValues();
        
        
    }
    
    public void MinusTech(int howMuchTech)
    {
        if (GameManager.instance.isPlayerOneTurn)
        {
            playerOneCurrentTechAmount -= howMuchTech;
        }
        else
        {
            playerTwoCurrentTechAmount -= howMuchTech;

        }
        UpdateValues();
    }

    public void AddTech(int howMuchTech)
    {
        if (GameManager.instance.isPlayerOneTurn)
        {
            playerOneCurrentTechAmount += howMuchTech;
        }
        else
        {
            playerTwoCurrentTechAmount += howMuchTech;

        }
        UpdateValues();
        
    }
    
    public void MinusArmySize(int howMuchArmySize)
    {
        if (GameManager.instance.isPlayerOneTurn)
        {
            playerOneCurrentArmySize -= howMuchArmySize;
        }
        else
        {
            playerTwoCurrentArmySize -= howMuchArmySize;

        }
        UpdateValues();
    }

    public void AddArmySize(int howMuchArmySize)
    {
        if (GameManager.instance.isPlayerOneTurn)
        {
            playerOneCurrentArmySize += howMuchArmySize;
        }
        else
        {
            playerTwoCurrentArmySize += howMuchArmySize;

        }
        
        UpdateValues();
        
    }

    private void UpdateValues()
    {
        for (int i = 0; i < playerOneGoldTexts.Count; i++)
        {
                    playerOneGoldTexts[i].text = playerOneCurrentGold.ToString();

        }
        
        for (int i = 0; i < playerTwoGoldTexts.Count; i++)
        {
            playerTwoGoldTexts[i].text = playerTwoCurrentGold.ToString();

        }
        
        
        for (int i = 0; i < playerOneTechTexts.Count; i++)
        {
            playerOneTechTexts[i].text = playerOneCurrentTechAmount.ToString();

        }
        
        for (int i = 0; i < playerTwoTechTexts.Count; i++)
        {
            playerTwoTechTexts[i].text = playerTwoCurrentTechAmount.ToString();

        }
        
        for (int i = 0; i < playerOneArmySizeTexts.Count; i++)
        {
            playerOneArmySizeTexts[i].text = playerOneCurrentArmySize.ToString();

        }
        
        for (int i = 0; i < playerTwoArmySizeTexts.Count; i++)
        {
            playerTwoArmySizeTexts[i].text = playerTwoCurrentArmySize.ToString();

        }

    }
}
