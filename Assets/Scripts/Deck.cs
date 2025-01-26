using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    public List<DeckData> tulipDeckData;
    public List<EventDeckData> eventDeckData;
    private List<Card> tulipDeck;
    private List<EventCard> eventDeck;
    // Start is called before the first frame update
    void Awake()
    {
        GenerateTulipDeck();
        GenerateEventDeck();
    }
    public void GenerateTulipDeck()
    {
        tulipDeck = new List<Card>();   
        foreach(DeckData deckData in tulipDeckData)
        {
            for(int i=0;i<deckData.cardCount;i++)
            {
                Card card = new Card(deckData.cardData);
                tulipDeck.Add(card);
            }
        }
        Helpers.Shuffle(tulipDeck);
    }

    public Card GetCardFromTulipDeck()
    {
        if(tulipDeck.Count>0)
        {
            Card toReturn = tulipDeck[0];
            tulipDeck.RemoveAt(0);
            return toReturn;            
        }
        return null;
    }
    public void GenerateEventDeck()
    {
        eventDeck = new List<EventCard>();
        foreach (EventDeckData deckData in eventDeckData)
        {
            for (int i = 0; i < deckData.cardCount; i++)
            {
                EventCard card = new EventCard(deckData.cardData);
                eventDeck.Add(card);
            }
        }
        Helpers.Shuffle(eventDeck);
    }
    public EventCard GetCardFromEventDeck()
    {
        if (eventDeck.Count > 0)
        {
            EventCard toReturn = eventDeck[0];
            eventDeck.RemoveAt(0);
            return toReturn;
        }
        return null;
    }
}
[Serializable]
public class DeckData
{
    public int cardCount = 3;
    public CardData cardData;
}


[Serializable]
public class EventDeckData
{
    public int cardCount = 3;
    public EventCardData cardData;
}
