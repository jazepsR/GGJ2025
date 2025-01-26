using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventCard : MonoBehaviour
{
    private EventCardData cardData;
    private CardVisuals cardVisuals;
    private Player localPlayer;
    public EventCard(EventCardData cardData)
    {
        this.cardData = cardData;
    }

    public EventCardData GetEventCardData()
    {
        return this.cardData;
    }
}
