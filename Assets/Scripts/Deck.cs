using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    public List<DeckData> tulipDeckData;
    private List<Card> tulipDeck;
    // Start is called before the first frame update
    void Awake()
    {
        GenerateTulipDeck();
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
    
}
[Serializable]
public class DeckData
{
    public int cardCount = 3;
    public CardData cardData;

}
