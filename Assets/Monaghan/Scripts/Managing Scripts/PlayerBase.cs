using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBase : MonoBehaviour
{
    private float tileColumnMax;
    private float tileRowMax;
    private Quaternion zoomedInOrientationPlayerOne = Quaternion.Euler(90, 0, 0);
    private Quaternion zoomedInOrientationPlayerTwo = Quaternion.Euler(90, 180, 0);

    private Vector3 zoomedPositionPlayerOne = new Vector3(1.72f, 3.94f, 1.71f);
    private Vector3 zoomedPositionPlayerTwo = new Vector3(4.27f, 3.94f, 2.3f);

    public int CurrentX { set; get;}
    public int CurrentY { set; get;}
    public bool mouseOver = false;
    public bool isPlayerOneCard;
    //variables for testing mouse over
    private Color startColor = Color.white;
    private Color mouseOverColor = new Color(1,1,1,0.5f);
    
    public enum CardState{InDeck, InHand, InPlay}
    [SerializeField] private CardState cardState;
    private GameObject cardClone;
    
    public Vector3 CurrentPosition;


    public float raycastDistance;
    public LayerMask layersToHit;
        
    public void SetPosition(int x, int y)
    {
        CurrentX = x;
        CurrentY = y;
    }
    
    private float mZCoord;

    public Card cardReference;


    private void Start()
    {
        //Generate the numbers for tile column and row max numbers
        tileColumnMax = BoardManager.instance.tileColumnNumber;
        tileRowMax = BoardManager.instance.tileRowNumber;
        cardReference = GetComponent<CardDisplay>().card;
        if (cardReference == null)
        {
            Debug.LogError("[PlayerBase] NO CARD DISPLAY!! Check your references");
        }

        EndTurn.OnButtonPush += DoDamage;
        EndTurn.OnApplyDamage += DoApplyDamage;

    }

    void OnDestroy()
    {
        EndTurn.OnButtonPush -= DoDamage;
        EndTurn.OnApplyDamage -= DoApplyDamage;
    }


    private void OnMouseEnter()
    {
        
        Debug.Log("mouseentered: " + gameObject.name);
        Debug.Log("absolute mouse = " + Input.mousePosition);
        
        if ((cardState == CardState.InHand && GameManager.instance.isPlayerOneTurn == isPlayerOneCard) ||
            cardState == CardState.InPlay)
        {


            mouseOver = true;
            //make card show up larger and flip the right way if needed
            if (cardClone != null)
            {
                Destroy(cardClone);
            }
            //if statement here changiing for player one or two and giving different orientation values
            if (GameManager.instance.isPlayerOneTurn)
            {
                cardClone = Instantiate(gameObject, zoomedPositionPlayerOne, zoomedInOrientationPlayerOne);
                //so it didnt get stuck
                Destroy(cardClone.GetComponent<CardDisplay>());
                //GetComponent<Renderer>().material.SetColor("_Color", mouseOverColor);
            }
            else
            {
                cardClone = Instantiate(gameObject, zoomedPositionPlayerTwo, zoomedInOrientationPlayerTwo);
                //GetComponent<Renderer>().material.SetColor("_Color", mouseOverColor);
            }
            Debug.Log("hovered");

        }

    }

    private void OnMouseExit()
    {

        if ((cardState == CardState.InHand && GameManager.instance.isPlayerOneTurn == isPlayerOneCard) ||
            cardState == CardState.InPlay)
        {
            mouseOver = false;
            //GetComponent<Renderer>().material.SetColor("_Color", startColor);
        }
        Destroy(cardClone);
        
    }

    void OnMouseDown()
    {
        mZCoord = GameManager.instance.playerOneCamera.WorldToScreenPoint(gameObject.transform.position).z;
    }

    private Vector3 GetMouseWorldPos()
    {


        Vector3 mousePoint = Input.mousePosition;
        
        if (!Application.isEditor && !GameManager.instance.isPlayerOneTurn)
        {
            mousePoint.x += Screen.width;
        }
        mousePoint.z = mZCoord;

        return GameManager.instance.currentCamera.ScreenToWorldPoint(mousePoint);
    }

    void OnMouseDrag()
    {
        if (cardState == CardState.InHand && GameManager.instance.isPlayerOneTurn == isPlayerOneCard)
        {
            transform.position = GetMouseWorldPos();
        }
        
    }

    private void OnMouseUp()
    {
        //If the card you are clicking isn't a card from in your hand then run this 
        if (cardState == CardState.InHand && GameManager.instance.isPlayerOneTurn == isPlayerOneCard)
        {
            if (isPlayerOneCard)
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
                
                int layer = LayerMask.NameToLayer("Default");

                gameObject.layer = layer;
                foreach (Transform child in transform)
                {
                    child.gameObject.layer = layer;
                }
                
                Debug.Log("[PlayerBase] card(" + gameObject.name + ") is being set to " + targetState );
                
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(targetState), targetState, null);
            
            
        }
        
        
        
    }


    private RaycastHit hitInfo;
    public void DoDamage()
    {
        if (cardState == CardState.InPlay)
        {
            
            //Debug.Log(gameObject.name + " WANNA HIT SOMETHING");

            if (Physics.Raycast(transform.position, transform.up, out hitInfo, raycastDistance, layersToHit))
            {
                GameObject hitObject = hitInfo.transform.gameObject;
                
                //Debug.Log(gameObject.name + " HIT SOMETHING");

                if (hitObject.tag == "Card")
                {
                    //Debug.Log(gameObject.name + " HIT A CARD");
                    
                    hitObject.GetComponent<CardDisplay>().health -= cardReference.attack;
                }
                else if (hitObject.tag == "Building")
                {
                    //Debug.Log(gameObject.name + " HIT A BUILDING");

                    hitObject.GetComponentInParent<BuildingManager>().TakeDamage(cardReference.attack);

                }

            } 
            else
            {
                Debug.Log(gameObject.name + " HIT DA BASE");

                if (isPlayerOneCard)
                {
                    BoardManager.instance.PlayerTwoBase.TakeDamage(cardReference.attack);
                }
                else
                {
                    BoardManager.instance.PlayerOneBase.TakeDamage(cardReference.attack);
                }
                    
            }
        }

    }

    public void DoApplyDamage()
    {

        if (cardState == CardState.InPlay)
        {
            if (GetComponent<CardDisplay>().health <= 0)
            {
                Destroy(gameObject);
                //Remove from list

            }
            else
            {
                GetComponent<CardDisplay>().healthText.text = GetComponent<CardDisplay>().health.ToString();
            }

        }

    }
    
    
}
