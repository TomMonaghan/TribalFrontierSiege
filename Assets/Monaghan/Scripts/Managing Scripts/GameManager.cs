using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Quaternion CameraEndTurnFlip = Quaternion.Euler(90,180 , 180);
    public static GameManager Instance;
    public bool isPlayerOneTurn = true;
    
    public void Start()
    {
        isPlayerOneTurn = true;
        Instance = this;
    }

    public void EndTurn()
    {
        //Change the players turn and flip the camera around
        isPlayerOneTurn = !isPlayerOneTurn;
        MainCamera.instance.gameObject.transform.Rotate(new Vector3(0, 0, 180));
        //timer to trigger this
        
    }
}
