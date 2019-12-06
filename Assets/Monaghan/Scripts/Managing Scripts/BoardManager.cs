using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour
{
    
    
    public static BoardManager instance;
    public PlayerBase[,] Cards { set; get;}
    private PlayerBase selectedCard; 
    
    private const float tileSize = 1.0f;
    private const float tileOffset = 0.5f;
    private const float heightOffset = 0.0f;
    
    private int selectionX = -1;
    private int selectionY = -1;

    
    [Header("References")]
    
    public List<GameObject> objectPrefabs;
    public List<GameObject> playerOneCardPrefabs;
    public List<GameObject> playerTwoCardPrefabs;
    
    
    [Header("Controls")]
 

    //To keep track of which tile you're selecting so you know which tile you're hovering over
    public float handSpacing;

    public int tileColumnNumber = 4;
    public int tileRowNumber = 6;
    [SerializeField]
    private int deckSize = 25;
    [SerializeField]
    private int startingHandSize = 4;
    public int playerOneHandSize = 0;
    public int playerTwoHandSize = 0;
    public int maximumHandSize = 10;
    public int playerOneCurrentGold;
    public int playerOneCurrentTechAmount;
    public int playerOneCurrentArmySize;
    public int playerTwoCurrentGold;
    public int playerTwoCurrentTechAmount;
    public int playerTwoCurrentArmySize;
    
    public float columnLineLength = 2.0f;
    public float rowLineLength = 4.5f;
    public Vector3 widthLine;
    public Vector3 heightLine;


    //Which way the objects are facing in the SpawnBasesOnBoard function
    private Quaternion YourCardsFaceUpCardOrientation = Quaternion.Euler(90, 0, 0);
    private Quaternion EnemyCardsFaceUpCardOrientation = Quaternion.Euler(90, 180, 0);
    private Quaternion YourCardsFaceDownCardOrientation = Quaternion.Euler(90,180 , 180);
    private Quaternion baseOrientation = Quaternion.Euler(0, 0, 0);


    [Header("Read Only")]
    
    
    public List<GameObject> playerOneHand;
    public List<GameObject> playerTwoHand;
    public List<GameObject> playerOne;
    
    public List<GameObject> playerOneDeck;
    public List<GameObject> playerTwoDeck;
    public List<GameObject> playerOneInPlay;
    public List<GameObject> playerTwoInPlay;
    private List<GameObject> activeObject = new List<GameObject>();
    private List<GameObject> activeCard = new List<GameObject>();

    public MainBases PlayerOneBase;
    public MainBases PlayerTwoBase;

    private void Start()
    {
        instance = this;
        //spawn a tech building (make it in a position you click
        //SpawnTechBuilding.OnButtonPush += SpawnTechBuildingCurrentPlayer;
        //SpawnBarracksBuilding.OnButtonPush += SpawnBarracksBuildingCurrentPlayer;
        //switch turns
        EndTurn.OnButtonPush += EndCurrentPlayerTurn;
        //spawn the two main bases
        SpawnMainBases();
        //spawn both players decks and shuffle them
        SpawnPlayerOneDeck();
        SpawnPlayerTwoDeck();
        //Draw the starting hand for both players
        PlayerOneDrawCard(startingHandSize);
        GameManager.instance.isPlayerOneTurn = false;
        PlayerTwoDrawCard(startingHandSize);
        GameManager.instance.isPlayerOneTurn = true; 
        
        //Making the inplay array slots all have null in them so cards can be placed in
        playerOneInPlay = new List<GameObject>();
        for (int i = 0; i < tileColumnNumber; i++)
        {
            playerOneInPlay.Add(null);
        }
        playerTwoInPlay = new List<GameObject>();
        for (int i = 0; i < tileColumnNumber; i++)
        {
            playerTwoInPlay.Add(null);
        }
    }
    private void OnDestroy()
    {
       
        EndTurn.OnButtonPush -= EndCurrentPlayerTurn;
        /////////////////////////////
        /// IS IT OKAY TO PUT THEM ALL IN HERE??????
        /// //////////////////////////////////
       //SpawnTechBuilding.OnButtonPush -= SpawnTechBuildingCurrentPlayer;
        //SpawnBarracksBuilding.OnButtonPush -= SpawnBarracksBuildingCurrentPlayer;

    }
    
    
    private void Update()
    {
        UpdateSelection();
        DrawGameboard();
        
    }
    
    //Draws white lines the make up the board grid
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
        
        //makes an x over the square that your mouse is in
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
    
    //Casts to the board hitting it and giving the mouse position in x and z values
    private void UpdateSelection()
    {
        if (!GameManager.instance.currentCamera)
            return;

        RaycastHit hit;
        if (Physics.Raycast(GameManager.instance.currentCamera.ScreenPointToRay(Input.mousePosition), out hit, 25.0f,
            LayerMask.GetMask("BoardPlane")))
        {
            
            selectionX = (int) hit.point.x;
            selectionY = (int) hit.point.z;// Debug.Log(hit.point);
        }
        else
        {
            selectionX = -1;
            selectionY = -1;

        }
        
    }
    //Spawn the 2 main bases onto the board
    private void SpawnBasesOnBoard(int index, int x, int y, int z, out MainBases baseReferenceToSave) 
         {
             GameObject go = Instantiate(objectPrefabs [index], GetTileLine(x, y, z), baseOrientation) as GameObject;
             go.transform.SetParent(transform);
             activeObject.Add(go);
             baseReferenceToSave = go.GetComponent<MainBases>();
         }
    
    private void SpawnSideBasesOnBoard(int index, int x, int y, int z) 
    {
        GameObject go = Instantiate(objectPrefabs [index], GetTileCentre(x, y, z), baseOrientation) as GameObject;
        go.transform.SetParent(transform);
        activeObject.Add(go);
    }
    
    //Used to spawn the bases in the centre
    private Vector3 GetTileLine(int x, int y, int z)
    {
        Vector3 origin = Vector3.zero;
        origin.x += (tileSize * x);
        origin.z += (tileSize * y) + tileOffset;
        origin.y += (tileSize * z);
        return origin; 
    }
    
    //Used in conjuction with the spawns to snap them into the centre of the squares
    private Vector3 GetTileCentre(int x, int y, int z)
    {
        Vector3 origin = Vector3.zero;
        origin.x += (tileSize * x) + tileOffset;
        origin.z += (tileSize * y) + tileOffset;
        origin.y += (tileSize * z);
        return origin; 
    }
    
    //Positioning the main bases at the top and bottom
    private void SpawnMainBases()
    {
        SpawnBasesOnBoard(0,3, 0, 0, out PlayerOneBase);
        SpawnBasesOnBoard(1, 3, 3, 0, out PlayerTwoBase);

    }
    
    //Spawn cards as ints if i want them to snap, aka summoning effects from minions spawning 
    private void SpawnCard(List<GameObject> cardList, int index, int x, int y, int z)
    {
        
        GameObject card = cardList[index];
        card.transform.SetParent(transform);
        card.transform.position = GetTileCentre(x, y, z);
        if (GameManager.instance.isPlayerOneTurn)
        {
            card.transform.rotation = YourCardsFaceUpCardOrientation;
        }
        else
        {
            card.transform.rotation = EnemyCardsFaceUpCardOrientation;
        }
    }
    
    //remade as vector 3 instead of ints so the cards in the hand could change position without needing to be in a snap spot
    private void SpawnCard(List<GameObject> cardList, int index, Vector3 position)
    {
        //puts the spawned card into whatever list was specified
        GameObject card = cardList[index];
        card.transform.SetParent(transform);
        //set position of spawn card
        card.transform.position = position;
        
        //setting which way the cards face when they spawn
        if (GameManager.instance.isPlayerOneTurn)
        {
            card.transform.rotation = YourCardsFaceUpCardOrientation;
        }
        else
        {
            card.transform.rotation = EnemyCardsFaceUpCardOrientation;
        }
    }
    
    //Spawning the cards into the game for player one at the deck spot, getting them from the prefab list
    private void SpawnPlayerOneCardsOnBoard(int index, int x, int y, int z) 
    {
        GameObject go = Instantiate(playerOneCardPrefabs [index], GetTileCentre(x, y, z), YourCardsFaceUpCardOrientation) as GameObject;
        go.transform.SetParent(transform);
        playerOneDeck.Add(go);
        
        int layer = LayerMask.NameToLayer("PlayerOneOnly");
        go.layer = layer;
        
        foreach (Transform child in go.transform)
        {
            if (child.gameObject.name == "CardBack") continue;
            child.gameObject.layer = layer;
            
        }
        
        go.GetComponent<CardDisplay>().InitialiseCard();
    }
    
    //Same as above for for player two
    private void SpawnPlayerTwoCardsOnBoard(int index, int x, int y, int z) 
    {
        GameObject go = Instantiate(playerTwoCardPrefabs [index], GetTileCentre(x, y, z), EnemyCardsFaceUpCardOrientation) as GameObject;
        go.transform.SetParent(transform);
        playerTwoDeck.Add(go);
        
        int layer = LayerMask.NameToLayer("PlayerTwoOnly");
        go.layer = layer;
        
        foreach (Transform child in go.transform)
        {
            if (child.gameObject.name == "CardBack") continue;
            child.gameObject.layer = layer;
            
        }
        
        go.GetComponent<CardDisplay>().InitialiseCard();
    }
    
    //Spawn the cards from the index up to how big the deck size is for player one, should be all the cards
    private void SpawnPlayerOneDeck()
    {
        for ( int i = 0; i < deckSize; i++)
        {
            SpawnPlayerOneCardsOnBoard(i, 6, 0, 1/8);
        }
        
        //shuffle list here
        ShuffleDeckPlayerOne();
    }
    
    //Spawn the cards from the index up to how big the deck size is for player one, should be all the cards
    private void SpawnPlayerTwoDeck()
    {
        for (int i = 0; i < deckSize; i++)
        {
            SpawnPlayerTwoCardsOnBoard(i, -1, 3, 1/8);
        }
        ShuffleDeckPlayerTwo();
        
    }
    
    //Draws the card for player one and sets the cards state to in hand, then adds it to the hand list
    //and removes it from the deck list, then increases that players hand size by one
    public void PlayerOneDrawCard(int numberOfCards)
    {
 
        for (int j = 0; j < numberOfCards; j++)
        {
            SpawnCard(playerOneDeck, 0, new Vector3(handSpacing * (j + 0.5f), 1/8, -0.5f));
            
            //setting card to know it's in the hand
            playerOneDeck[0].GetComponent<PlayerBase>().SetCardState(PlayerBase.CardState.InHand);
            
            playerOneHand.Add(playerOneDeck[0]);
            playerOneDeck.RemoveAt(0);
            playerOneHandSize++;
        }
        
        RestructureHandPlayerOne();


    }
    
    //Draws the card for player two and sets the cards state to in hand, then adds it to the hand list
    //and removes it from the deck list, then increases that players hand size by one
    public void PlayerTwoDrawCard(int numberOfCards)
    {
 
        for (int j = 0; j < numberOfCards; j++)
        {
            SpawnCard(playerTwoDeck, 0, new Vector3(tileColumnNumber - (handSpacing * (j + 0.5f)), 1/8, 4.5f));
            
            //setting card to know it's in the hand
            playerTwoDeck[0].GetComponent<PlayerBase>().SetCardState(PlayerBase.CardState.InHand);
            
            playerTwoHand.Add(playerTwoDeck[0]);
            playerTwoDeck.RemoveAt(0);
            playerTwoHandSize++;
        }

        RestructureHandPlayerTwo();

    }
    
    void EndCurrentPlayerTurn()
    {
        if (GameManager.instance.isPlayerOneTurn)
        {
            PlayerOneDrawCard(1);
        }
        else
        {
            PlayerTwoDrawCard(1);
        }
        
        
        
    }

    
    /////////////////////////////////////
    /// HOW TO BLOCK THIS BUTTON ONCE IT'S BEEN CLICKED
    /// ////////////////////////////
//    void SpawnTechBuildingCurrentPlayer()
//    {
//        //ifplayerone do index 3
//        //else do index 5
//        if (GameManager.instance.isPlayerOneTurn)
//        {   
//            /////////////////////////////////
//            ///HOW TO MAKE THE POSITION EQUAL TO THE TOP CENTRE OF THE OBJECT YOU CLICK
//            /// ////////////////////////////
//            SpawnSideBasesOnBoard(3,0, 0, 0);
//            playerOneCurrentTechAmount++;
//        }
//        else
//        {
//            /////////////////////////////////
//            ///HOW TO MAKE THE POSITION EQUAL TO THE TOP CENTRE OF THE OBJECT YOU CLICK
//            /// ////////////////////////////
//            SpawnSideBasesOnBoard(5,0, 0, 0);
//            playerTwoCurrentTechAmount++;
//        }
//    }
    
    /////////////////////////////////////
    /// HOW TO BLOCK THIS BUTTON ONCE IT'S BEEN CLICKED (LOWER PRIORITY IF NOT QUICK FIX)
    /// ////////////////////////////
//    void SpawnBarracksBuildingCurrentPlayer()
//    {
//        //ifplayerone do index 3
//        //else do index 5
//        if (GameManager.instance.isPlayerOneTurn)
//        {   
//            /////////////////////////////////
//            ///HOW TO MAKE THE POSITION EQUAL TO THE TOP CENTRE OF THE OBJECT YOU CLICK
//            /// ////////////////////////////
//            SpawnSideBasesOnBoard(2,0, 0, 0);
//            
//            playerOneCurrentArmySize++;
//            
//        }
//        else
//        {
//            /////////////////////////////////
//            ///HOW TO MAKE THE POSITION EQUAL TO THE TOP CENTRE OF THE OBJECT YOU CLICK
//            /// ////////////////////////////
//            SpawnSideBasesOnBoard(4,0, 0, 0);
//            playerTwoCurrentArmySize++;
//        }
//    }



//Randomize player two's deck
    public void ShuffleDeckPlayerOne()
    {
         for (int i = 0; i < playerOneDeck.Count; i++) 
         {
                 GameObject temp = playerOneDeck[i];
                 int randomIndex = Random.Range(i, playerOneDeck.Count);
                 playerOneDeck[i] = playerOneDeck[randomIndex];
                 playerOneDeck[randomIndex] = temp;
         }
    }
    
    //Randomize player 2's deck

    public void ShuffleDeckPlayerTwo()
    {
        for (int i = 0; i < playerTwoDeck.Count; i++) 
        {
            GameObject temp = playerTwoDeck[i];
            int randomIndex = Random.Range(i, playerTwoDeck.Count);
            playerTwoDeck[i] = playerTwoDeck[randomIndex];
            playerTwoDeck[randomIndex] = temp;
        }
    }

    //Changes the null spot in play to the card that is placed in there
    public void PlaceCardPlayerOne(GameObject card, int index)
    {
        playerOneInPlay[index] = card;
    }
    
    //Changes the null spot in play to the card that is placed in there
    public void PlaceCardPlayerTwo(GameObject card, int index)
    {
        playerTwoInPlay[index] = card;
    }

    //Removes the card that youre holding from the hand list while reducing hand size and bringing the cards in together
    public void RemoveFromHandPlayerOne(GameObject objectToRemove)
    {
        playerOneHand.RemoveAt(playerOneHand.IndexOf(objectToRemove));

        playerOneHandSize--;

        RestructureHandPlayerOne();

    }
    
    //Removes the card that youre holding from the hand list while reducing hand size and bringing the cards in together

    public void RemoveFromHandPlayerTwo(GameObject objectToRemove)
    {
        playerTwoHand.RemoveAt(playerTwoHand.IndexOf(objectToRemove));

        playerTwoHandSize--;

        RestructureHandPlayerTwo();

    }



     //does a check through the hand and makes it come together so they don't have spaced out gaps
     private void RestructureHandPlayerOne()
     {
         for (int i = 0; i < playerOneHand.Count; i++)
         {
             playerOneHand[i].transform.position = new Vector3( handSpacing * (i + 0.5f),
                                                                 playerOneHand[i].transform.position.y,
                                                                 playerOneHand[i].transform.position.z) ;

             playerOneHand[i].GetComponent<PlayerBase>().CurrentPosition = playerOneHand[i].transform.position;

         }
     }
     
     //does a check through the hand and makes it come together so they don't have spaced out gaps
     private void RestructureHandPlayerTwo()
     {
         for (int i = 0; i < playerTwoHand.Count; i++)
         {
             playerTwoHand[i].transform.position = new Vector3( tileColumnNumber - (handSpacing * (i + 0.5f)),
                                                                 playerTwoHand[i].transform.position.y,
                                                                 playerTwoHand[i].transform.position.z) ;
          
             playerTwoHand[i].GetComponent<PlayerBase>().CurrentPosition = playerTwoHand[i].transform.position;
             
         }
     }
}
