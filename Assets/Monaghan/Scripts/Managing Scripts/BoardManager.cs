﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour
{
    public PlayerBase[,] Cards { set; get;}
    private PlayerBase selectedCard; 
    
    private const float tileSize = 1.0f;
    private const float tileOffset = 0.5f;
    private const float heightOffset = 0.0f;

    //To keep track of which tile you're selecting so you know which tile you're hovering over
    private int selectionX = -1;
    private int selectionY = -1;
    [SerializeField]
    private int tileColumnNumber = 4;
    [SerializeField]
    private int tileRowNumber = 6;
    [SerializeField]
    private int deckSize = 25;
    [SerializeField]
    private int startingHandSize = 4;
    public float columnLineLength = 2.0f;
    public float rowLineLength = 4.5f;
    public Vector3 widthLine;
    public Vector3 heightLine;
    public List<GameObject> objectPrefabs;
    public List<GameObject> playerOneCardPrefabs;
    public List<GameObject> playerTwoCardPrefabs;
    public List<GameObject> playerOneHand;
    public List<GameObject> playerTwoHand;
    public List<GameObject> playerOneInPlay;
    public List<GameObject> playerTwoInPlay;
    private List<GameObject> activeObject = new List<GameObject>();
    private List<GameObject> activeCard = new List<GameObject>();
    //Which way the objects are facing in the SpawnBasesOnBoard function
    private Quaternion YourCardsFaceUpCardOrientation = Quaternion.Euler(90, 0, 0);
    private Quaternion EnemyCardsFaceUpCardOrientation = Quaternion.Euler(90, 180, 0);
    private Quaternion YourCardsFaceDownCardOrientation = Quaternion.Euler(90,180 , 180);
    private Quaternion baseOrientation = Quaternion.Euler(0, 0, 0);


    public bool isPlayerOneTurn = true;
    

    private void Start()
    {
        DrawCardPlayerOne.OnButtonPush += PlayerOneDrawCard;
        
        SpawnMainBases();
        //spawn deck lists and decks
        SpawnPlayerOneDeck();
        SpawnPlayerTwoDeck();
        //puut player one draw card into end turn and make it one instead of startinghandsize maybe need to include some kind of spawn as spawnplayeronestartinghand is needed here for the card objects
        //these two below take the card from the deck list and add it to the hand list
        PlayerOneDrawCard(startingHandSize);
        PlayerTwoDrawCard(startingHandSize);
        //spawn the card objects into the hand
        //    SpawnPlayerOneStartingHand();
        //SpawnPlayerTwoStartingHand();
        
    }

    private void OnDestroy()
    {
        DrawCardPlayerOne.OnButtonPush -= PlayerOneDrawCard;
    }

    private void Update()
    {
        UpdateSelection();
        DrawGameboard();

        if (Input.GetMouseButton(0))
        {
            if (selectionX >= 0 && selectionY >= 0)
            {
                if (selectedCard == null)
                {
                    //select the card
                }
                else
                {
                    //Move the card
                }
            }
        }
    }

    private void SelectCard(int x, int y)
    {
        if (Cards[x, y] == null)
            return;
        
//        if(Cards[x, y]).isPlayerOne != isPlayerOneTurn)
    }

    private void MoveCard(int x, int y)
    {
        
    }
    private void DrawGameboard()
    {
        Vector3 widthLine = (Vector3.right * tileRowNumber) * (rowLineLength / 3);
        Vector3 heightLine = (Vector3.forward * tileColumnNumber) * (columnLineLength / 3) ;

        for (int i = 0; i <= tileRowNumber; i++)
        {
            Vector3 start = Vector3.forward * i;
            
            Debug.DrawLine(start, start + widthLine);
            for (int j = 0; j <= tileColumnNumber; j++)
            {
                start = Vector3.right * j;
                Debug.DrawLine(start, start + heightLine);
            }
        }
        
        //debug cross for where your mouse is
        if (selectionX >= 0 && selectionY >= 0)
        {
            Debug.DrawLine(
                Vector3.forward * selectionY + Vector3.right * selectionX,
                Vector3.forward * (selectionY + 1) + Vector3.right * (selectionX + 1));
            
            Debug.DrawLine(
                Vector3.forward * (selectionY + 1) + Vector3.right * selectionX,
                Vector3.forward * selectionY + Vector3.right * (selectionX + 1));
                
        }

       
    }
    
    private void UpdateSelection()
    {
        if (!Camera.main)
            return;

        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 25.0f,
            LayerMask.GetMask("BoardPlane")))
        {
            
            selectionX = (int) hit.point.x;
            selectionY = (int) hit.point.z;
            Debug.Log(hit.point);
        }
        else
        {
            selectionX = -1;
            selectionY = -1;

        }
        
    }

    private void SpawnBasesOnBoard(int index, int x, int y, int z) 
         {
             GameObject go = Instantiate(objectPrefabs [index], GetTileLine(x, y, z), baseOrientation) as GameObject;
             go.transform.SetParent(transform);
             //Cards[x, y] = go.GetComponent<PlayerBase>();
             activeObject.Add(go);
         }
    
    private Vector3 GetTileLine(int x, int y, int z)
    {
        Vector3 origin = Vector3.zero;
        origin.x += (tileSize * x);
        origin.z += (tileSize * y) + tileOffset;
        origin.y += (tileSize * z);
        return origin; 
    }
    
   

    private Vector3 GetTileCentre(int x, int y, int z)
    {
        Vector3 origin = Vector3.zero;
        origin.x += (tileSize * x) + tileOffset;
        origin.z += (tileSize * y) + tileOffset;
        origin.y += (tileSize * z);
        return origin; 
    }
    
    private void SpawnMainBases()
    {
        SpawnBasesOnBoard(0,3, 0, 1/8);
        SpawnBasesOnBoard(1, 3, 3, 1/8);

    }
    
    
    private void SpawnCard(List<GameObject> cardList, int index, int x, int y, int z)
    {
        GameObject go = Instantiate(cardList[index], GetTileCentre(x, y, z), YourCardsFaceUpCardOrientation) as GameObject;
        go.transform.SetParent(transform);
    }
    
    private void SpawnPlayerOneCardsOnBoard(int index, int x, int y, int z) 
    {
        GameObject go = Instantiate(playerOneCardPrefabs [index], GetTileCentre(x, y, z), YourCardsFaceUpCardOrientation) as GameObject;
        go.transform.SetParent(transform);
        //change to 
    }
    
    private void SpawnPlayerTwoCardsOnBoard(int index, int x, int y, int z) 
    {
        GameObject go = Instantiate(playerTwoCardPrefabs [index], GetTileCentre(x, y, z), EnemyCardsFaceUpCardOrientation) as GameObject;
        go.transform.SetParent(transform);
        activeCard.Add(go);
    }
    
    private void SpawnPlayerOneDeck()
    {
        for ( int i = 0; i < deckSize - startingHandSize; i++)
        {
            SpawnPlayerOneCardsOnBoard(i + startingHandSize, 6, 0, 1/8);
        }
        
    }
    
    private void SpawnPlayerTwoDeck()
    {
        for (int i = 0; i < deckSize - startingHandSize; i++)
        {
            SpawnPlayerTwoCardsOnBoard(i + startingHandSize, -1, 3, 1/8);
        }
        
    }
   
    private void SpawnPlayerOneStartingHand()
    {
        for (int i = 0; i < startingHandSize; i++)
        {
            SpawnCard(playerOneHand, i, 0 + (i), -1, 1/8);
        }    
    }
    
    private void SpawnPlayerTwoStartingHand()
    {
        for (int i = 0; i < startingHandSize; i++)
        {
            SpawnPlayerTwoCardsOnBoard(i, 2 + (i) , 4, 1/8);
            
        }    
    }
    
    public void PlayerOneDrawCard(int i)
    {
 
        for (int j = 0; j < i; j++)
        {
            GameObject card = playerOneCardPrefabs[Random.Range(0, playerOneCardPrefabs.Count - 1)];

            playerOneHand.Add(card);
            playerOneCardPrefabs.Remove(card);
        }
        for (int k = 0; k < i; k++)
        {
            SpawnCard(playerOneHand, k, 0 + (k), -1, 1/8);
        }  

    }
    
    private void PlayerTwoDrawCard(int i)
    {
 
        for (int j = 0; j < i; j++)
        {
            GameObject card = playerTwoCardPrefabs[Random.Range(0, playerTwoCardPrefabs.Count - 1)];

            playerTwoHand.Add(card);
            playerTwoCardPrefabs.Remove(card);
        }
        for (int k = 0; k < i; k++)
        {
            SpawnCard(playerTwoHand, k, 2 + (k) , 4, 1/8);
        }  
    }

    


  
}
