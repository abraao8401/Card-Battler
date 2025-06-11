using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Card", order = 1)]
public class CardScriptableObject : ScriptableObject
{
    public string cardName;

    [TextArea]
    public string description;

    public string element, advantage, disadvantage;

    public int currentHealth, attackPower;

    public Sprite characterSprite, templateSprite;
}
