using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Quaternion CameraEndTurnFlip = Quaternion.Euler(90,180 , 180);
    public static GameManager instance;
    public bool isPlayerOneTurn = true;

    public Camera currentCamera;
    public Camera playerOneCamera;
    public Camera playerTwoCamera;

    
    public void Start()
    {
        
        // Check the number of monitors connected.
        
        if (Display.displays.Length > 1)
        {
            // Activate the display 1 (second monitor connected to the system).
            Display.displays[1].Activate();
        }
        
        isPlayerOneTurn = true;
        instance = this;
        currentCamera = playerOneCamera;
        currentCamera.tag = "MainCamera";
    }

    public void EndTurn()
    {
        //Change the players turn and flip the camera around
        isPlayerOneTurn = !isPlayerOneTurn;

        if (!Application.isEditor)
        {

            currentCamera.tag = "Untagged";

            //MainCamera.instance.gameObject.transform.Rotate(new Vector3(0, 0, 180));
            currentCamera = isPlayerOneTurn ? playerOneCamera : playerTwoCamera;

            currentCamera.tag = "MainCamera";

        }

//        if (currentCamera == playerOneCamera)
//        {
//            playerOneCamera.gameObject.SetActive(true);
//            playerTwoCamera.gameObject.SetActive(false);
//        }
//        else
//        {
//            playerTwoCamera.gameObject.SetActive(true);
//            playerOneCamera.gameObject.SetActive(false);
//        }
        //timer to trigger this

    }
}
