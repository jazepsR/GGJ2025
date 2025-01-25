using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ColorType { Red, Purple, Yellow};
public enum Rarity { I, II, III };
[CreateAssetMenu(fileName = "Tulip", menuName = "ScriptableObjects/Create tulip data", order = 1)]
public class CardData : ScriptableObject
{
    public string Name;
    public Sprite preview;
    public ColorType color;
    public Rarity rarity;
}
