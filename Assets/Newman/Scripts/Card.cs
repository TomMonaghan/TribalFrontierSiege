using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Cards/Creature")]
public class Card : ScriptableObject
{
    public string cardName;
    public string cardDescription;
    public string cardType;

    public int goldCost;
    public int techCost;
    public int attack;
    public int health;


}

[CreateAssetMenu(fileName = "New Card", menuName = "Cards/Spell")]
public class SpellCard : ScriptableObject
{
    public string cardName;
    public string cardDescription;
    public string cardType;

    public int goldCost;
    public int techCost;
    


}