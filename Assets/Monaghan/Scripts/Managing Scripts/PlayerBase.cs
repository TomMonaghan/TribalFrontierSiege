using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBase : MonoBehaviour
{
    private float tileColumnMax;
    private float tileRowMax;
    public int CurrentX { set; get;}
    public int CurrentY { set; get;}

    public bool isPlayerOne;
    
    public enum CardState{InDeck, InHand, InPlay}
    [SerializeField] private CardState cardState;
    
    
    public Vector3 CurrentPosition;
        
    public void SetPosition(int x, int y)
    {
        CurrentX = x;
        CurrentY = y;
    }
    
    private float mZCoord;


    private void Start()
    {
        //Generate the numbers for tile column and row max numbers
        tileColumnMax = BoardManager.instance.tileColumnNumber;
        tileRowMax = BoardManager.instance.tileRowNumber;
    }


    void OnMouseDown()
    {
        mZCoord = MainCamera.instance.WorldToScreenPoint(gameObject.transform.position).z;
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;

        mousePoint.z = mZCoord;

        return MainCamera.instance.ScreenToWorldPoint(mousePoint);
    }

    void OnMouseDrag()
    {
        if (cardState == CardState.InHand && GameManager.Instance.isPlayerOneTurn == isPlayerOne)
        {
            transform.position = GetMouseWorldPos();
        }
        
    }

    private void OnMouseUp()
    {
        //If the card you are clicking isn't a card from in your hand then run this 
        if (cardState == CardState.InHand)
        {
            if (isPlayerOne)
            {
                //If it's within player one combat zone then snap to the square you're in 
                if (GetMouseWorldPos().x < tileColumnMax && GetMouseWorldPos().x > 0 &&
                    GetMouseWorldPos().z > 1 && GetMouseWorldPos().z < 2 &&
                    BoardManager.instance.playerOneInPlay[(int) GetMouseWorldPos().x] == null)
                {

                    transform.position = new Vector3((int) GetMouseWorldPos().x + 0.5f, GetMouseWorldPos().y,
                        (int) GetMouseWorldPos().z + 0.5f);


                    BoardManager.instance.PlaceCardPlayerOne(gameObject, (int) GetMouseWorldPos().x);

                    BoardManager.instance.RemoveFromHandPlayerOne(gameObject);
                    SetCardState(CardState.InPlay);

                }
                //otherwise return card to spot it came from
                else
                {
                    transform.position = CurrentPosition;
                }

            }
            else
            {
                //if player 2 and it's within player one combat zone then snap to the square you're in 
                if (GetMouseWorldPos().x < tileColumnMax && GetMouseWorldPos().x > 0 &&
                    GetMouseWorldPos().z > 2 && GetMouseWorldPos().z < 3 &&
                    BoardManager.instance.playerTwoInPlay[(int) GetMouseWorldPos().x] == null)
                {
                    transform.position = new Vector3((int) GetMouseWorldPos().x + 0.5f, GetMouseWorldPos().y,
                        (int) GetMouseWorldPos().z + 0.5f);

                    BoardManager.instance.PlaceCardPlayerTwo(gameObject, (int) GetMouseWorldPos().x);


                    BoardManager.instance.RemoveFromHandPlayerTwo(gameObject);
                    SetCardState(CardState.InPlay);
                }
                //otherwise return card to spot it came from
                else
                {
                    transform.position = CurrentPosition;
                }
            }
        }
    }

    //can set cards to in deck, hand, or in play
    public void SetCardState(CardState targetState)
    {
        cardState = targetState;
        switch (cardState)
        {
            case CardState.InDeck:
                
                break;
            case CardState.InHand:

                CurrentPosition = transform.position;
                
                Debug.Log("[PlayerBase] card(" + gameObject.name + ") is being set to " + targetState );
                
                break;
            case CardState.InPlay:
                
                CurrentPosition = transform.position;
                
                Debug.Log("[PlayerBase] card(" + gameObject.name + ") is being set to " + targetState );
                
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(targetState), targetState, null);
            
            
        }
        
        
        
    }
    
    
    
}
