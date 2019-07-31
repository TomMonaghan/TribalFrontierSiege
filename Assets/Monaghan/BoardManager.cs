using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
   
    private const float tileSize = 2.0f;
    private const float tileOffset = 1.0f;
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
    
    
    
    public List<GameObject> basePrefabs;
    private List<GameObject> activeBase = new List<GameObject>();

    private void Start()
    {
        //SpawnMainBase(0, CentreMainBase(0,0));
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

    private void SpawnMainBase(int index, Vector3 position)
    {
        GameObject go = Instantiate(basePrefabs [index], position, Quaternion.identity) as GameObject;
        go.transform.SetParent(transform);
        activeBase.Add(go);
    }

    private Vector3 CentreMainBase(int x, int y)
    {
        Vector3 origin = Vector3.zero;
        origin.x += (tileSize * x) + tileOffset;
        origin.z += (tileSize * y) + tileOffset;
        return origin; 
    }
    
   
}
/*Haigens Way
 *  private void DrawGameboard()
    {
         widthLine = (Vector3.right * tileRowNumber) * (rowLineLength / 3);
         heightLine = (Vector3.forward * tileColumnNumber) * (columnLineLength / 3) ;

        for (int i = 0 - (tileRowNumber / 2); i <= tileRowNumber / 2; i++)
        {
             start = (Vector3.forward - widthLine /i) *i ;
            
            Debug.DrawLine(start, start + widthLine);
            for (int j = 0 - (tileColumnNumber / 2); j <= tileColumnNumber / 2; j++)
            {
                start = Vector3.right * j;
                Debug.DrawLine(start, start + heightLine);
            }
        }
        
        //Puts the marker over which square the mouse is on
        if (selectionX >= 0 && selectionY >= 0)
        {
            Debug.DrawLine(
                Vector3.forward * selectionY + Vector3.right * selectionX,
                Vector3.forward * (selectionY + 1) + Vector3.right * (selectionX + 1));
            
            Debug.DrawLine(
                Vector3.forward * (selectionY + 1) + Vector3.right * selectionX,
                Vector3.forward * selectionY + Vector3.right * (selectionX + 1));
                
        }
 */