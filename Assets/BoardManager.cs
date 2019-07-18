using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    // Start is called before the first frame update
    private const float tileSize = 2.0f;
    private const float tileOffset = 1.0f;
    //To keep track of which tile you're selecting so you know which tile you're hovering over
    private int selectionX = -1;
    private int selectionY = -1;
    private int tileRowNumber = 6;
    private int tileColumnNumber = 4;
    
    
    private void Update()
    {
        DrawChessboard();
    }

    private void DrawChessboard()
    {
        Vector3 widthLine = Vector3.right * tileRowNumber;
        Vector3 heightLine = Vector3.forward * tileColumnNumber;

        for (int i = 0; i <= tileRowNumber; i++)
        {
            Vector3 start = Vector3.forward * i;
            Debug.DrawLine(start, start + widthLine,);
            for (int j = 0; j <= tileColumnNumber; j++)
            {
                start = Vector3.right * j;
                Debug.DrawLine(start, start +heightLine);
            }
        }
    }
    

}
