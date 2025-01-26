using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EventCardType { Yellow, Purple, Red, Boom, Bust, Crash, Surge, Boost, CashInflux }
[CreateAssetMenu(fileName = "Event card", menuName = "ScriptableObjects/Create event card", order = 2)]
public class EventCardData : ScriptableObject
{
    public EventCardType cardType;
    public Sprite preview;
    public string displayName, description;    
}
