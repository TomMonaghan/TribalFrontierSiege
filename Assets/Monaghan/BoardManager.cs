using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
   
    private const float tileSize = 2.0f;
    private const float tileOffset = 1.0f;
    //To keep track of which tile you're selecting so you know which tile you're hovering over
    private int selectionX = -1;
    private int selectionY = -1;
    private int tileRowNumber = 6;
    private int tileColumnNumber = 4;
    public float columnLineLength = 1.5f;
    public float rowLineLength = 1.5f;
    
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
                Debug.DrawLine(start, start +heightLine);
            }
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
            Debug.Log(hit.point);
        }
    }

}
