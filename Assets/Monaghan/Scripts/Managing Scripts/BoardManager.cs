using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

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
    private int i = 0;
    public float columnLineLength = 2.0f;
    public float rowLineLength = 4.5f;
    public Vector3 widthLine;
    public Vector3 heightLine;
    public List<GameObject> objectPrefabs;
    public List<GameObject> playerOneCardPrefabs;
    public List<GameObject> playerTwoCardPrefabs;
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
        SpawnMainBases();
        SpawnCard();
        SpawnPlayerOneDeck();
        SpawnPlayerOneStartingHand();
        SpawnPlayerTwoDeck();
        SpawnPlayerTwoStartingHand();
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
    private void SpawnCard()
    {
        //SpawnPlayerOneCardsOnBoard(0, 0, 1, 1/8);
    }
    private void SpawnMainBases()
    {
        SpawnBasesOnBoard(0,3, 0, 1/8);
        SpawnBasesOnBoard(1, 3, 3, 1/8);

    }
    
    private void SpawnPlayerOneCardsOnBoard(int index, int x, int y, int z) 
    {
        GameObject go = Instantiate(playerOneCardPrefabs [index], GetTileCentre(x, y, z), YourCardsFaceUpCardOrientation) as GameObject;
        go.transform.SetParent(transform);
        //Cards[x, y] = go.GetComponent<PlayerBase>();
        activeCard.Add(go);
    }
    
    private void SpawnPlayerTwoCardsOnBoard(int index, int x, int y, int z) 
    {
        GameObject go = Instantiate(playerTwoCardPrefabs [index], GetTileCentre(x, y, z), EnemyCardsFaceUpCardOrientation) as GameObject;
        go.transform.SetParent(transform);
        activeCard.Add(go);
    }
    
    private void SpawnPlayerOneDeck()
    {
        for (i = 0; i < deckSize - startingHandSize; i++)
        {
            SpawnPlayerOneCardsOnBoard(i + startingHandSize, 6, 0, 1/8);
        }
        
    }
    
    private void SpawnPlayerTwoDeck()
    {
        for (i = 0; i < deckSize - startingHandSize; i++)
        {
            SpawnPlayerTwoCardsOnBoard(i + startingHandSize, -1, 3, 1/8);
        }
        
    }
   
    private void SpawnPlayerOneStartingHand()
    {
        for (i = 0; i < startingHandSize; i++)
        {
            SpawnPlayerOneCardsOnBoard(i, 0 + (i) , -1, 1/8);
            
        }    
    }
    
    private void SpawnPlayerTwoStartingHand()
    {
        for (i = 0; i < startingHandSize; i++)
        {
            SpawnPlayerTwoCardsOnBoard(i, 2 + (i) , 4, 1/8);
            
        }    
    }
    
    

    /*
    public class BaseHealth
    {
        public int CalculateBaseHealth(int baseUpgrades, int damageTaken, int baseHealing)
        {
            int health = 20;

            health += baseUpgrades * 5;
            health += baseHealing;
            health -= damageTaken;

            return health;
        }
    }*/
}
