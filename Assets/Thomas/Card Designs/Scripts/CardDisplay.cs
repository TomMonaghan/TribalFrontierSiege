using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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


    // Start is called before the first frame update
    void Start()
    {
        cardNameText.text = card.cardName;
        cardDescriptionText.text = card.cardDescription;
        cardTypeText.text = card.cardType;

        goldCostText.text = card.goldCost.ToString();
        techCostText.text = card.techCost.ToString();
        attackText.text = card.attack.ToString();
        healthText.text = card.health.ToString();
    }
}
