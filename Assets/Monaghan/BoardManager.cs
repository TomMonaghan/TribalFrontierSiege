using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public CreatureCard[,] CreatureCards { set; get;}
    private CreatureCard selectedCreatureCard; 
    
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
  
    public float columnLineLength = 2.0f;
    public float rowLineLength = 4.5f;

    public Vector3 widthLine;
    public Vector3 heightLine;
    
    
    
    public List<GameObject> objectPrefabs;
    private List<GameObject> activeObject = new List<GameObject>();
    //Which way the objects are facing in the SpawnBasesOnBoard function
    private Quaternion orientation = Quaternion.Euler(0, 0, 0);

    private void Start()
    {
        SpawnMainBases();
    }
    private void Update()
    {
        UpdateSelection();
        DrawGameboard();
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
             GameObject go = Instantiate(objectPrefabs [index], GetTileCentre(x, y, z), orientation) as GameObject;
             go.transform.SetParent(transform);
             
             activeObject.Add(go);
         }
    
    //work on creatures being able to be spawned and moved
    
    private Vector3 GetTileCentre(int x, int y, int z)
    {
        Vector3 origin = Vector3.zero;
        origin.x += (tileSize * x);
        origin.z += (tileSize * y) + tileOffset;
        origin.y += (tileSize * z);
        return origin; 
    }

    private void SpawnCreatureCard()
    {
        activeObject = new List<GameObject>();
        CreatureCards = new CreatureCard[6, 4];
        SpawnBasesOnBoard(0,7/2, 0, 1/8);
        SpawnBasesOnBoard(1, 7/2, 7/2, 1/8);
    }
    private void SpawnMainBases()
    {
        SpawnBasesOnBoard(0,7/2, 0, 1/8);
        SpawnBasesOnBoard(1, 7/2, 7/2, 1/8);
    }
   
}
