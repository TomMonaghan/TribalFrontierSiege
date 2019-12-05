using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private Quaternion CameraEndTurnFlip = Quaternion.Euler(90,180 , 180);
    public static GameManager instance;
    public bool isPlayerOneTurn = true;

    public Camera currentCamera;
    public Camera playerOneCamera;
    public Camera playerTwoCamera;

    public GameObject GameElements;
    public SpriteRenderer GameOverSpritePlayerOneRenderer;
    public SpriteRenderer GameOverSpritePlayerTwoRenderer;
    public Sprite playerOneWinGfx;
    public Sprite playerTwoWinGfx;
    
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

//        
        //timer to trigger this

    }

    public void DisplayEndScreen (bool playerOneWins)
    {
        // Set win graphic sprite
        GameOverSpritePlayerOneRenderer.sprite = playerOneWins ? playerOneWinGfx : playerTwoWinGfx;
        GameOverSpritePlayerTwoRenderer.sprite = playerOneWins ? playerOneWinGfx : playerTwoWinGfx;
        
        GameOverSpritePlayerOneRenderer.gameObject.SetActive(true);
        GameOverSpritePlayerTwoRenderer.gameObject.SetActive(true);
        
        GameElements.SetActive(false);

        StartCoroutine(WaitForEnd());

    }


    IEnumerator WaitForEnd()
    {
        while (true)
        {

            if (Input.GetKeyDown((KeyCode.Space)))
            {
                SceneManager.LoadScene("Game");
            }

            yield return null;

        }
    }
}
