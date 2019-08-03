using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpellCardDisplay : MonoBehaviour
{
    public SpellCard card;

    public TextMeshPro cardNameText;
    public TextMeshPro cardDescriptionText;
    public TextMeshPro cardTypeText;
    public TextMeshPro goldCostText;
    public TextMeshPro techCostText;

    // Start is called before the first frame update
    void Start()
    {
        cardNameText.text = card.cardName;
        cardDescriptionText.text = card.cardDescription;
        cardTypeText.text = card.cardType;
        goldCostText.text = card.goldCost.ToString();
        techCostText.text = card.techCost.ToString();
    }
}
