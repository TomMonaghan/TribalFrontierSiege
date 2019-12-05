using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

//using UnityEngine.UI;
public class CardDisplay : MonoBehaviour
{

    public Card card;
    

    public TextMeshPro cardNameText;
    public TextMeshPro cardDescriptionText;
    public TextMeshPro cardTypeText;
    public TextMeshPro goldCostText;
    public TextMeshPro techCostText;
    public TextMeshPro attackText;
    public TextMeshPro healthText;
    public SpriteRenderer cardArt;

    public float goldCost;
    public float techCost;
    public float attack;
    public float health;

    // Start is called before the first frame update
    

    public void InitialiseCard()
    {
        cardNameText.text = card.cardName;
        cardDescriptionText.text = card.cardDescription;
        cardTypeText.text = card.cardType;
        cardArt.sprite = card.artwork;
        goldCostText.text = card.goldCost.ToString();
        techCostText.text = card.techCost.ToString();
        attackText.text = card.attack.ToString();
        healthText.text = card.health.ToString();

        goldCost = card.goldCost;
        techCost = card.techCost;
        attack = card.attack;
        health = card.health;

    }
}
