using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class Card : MonoBehaviour
{
    private CardData cardData;
    private CardVisuals cardVisuals;
    private Player localPlayer;
    public Card(CardData cardData)
    {
        this.cardData = cardData;
    }

    public void SetupCardVisuals(CardVisuals cardVisuals)
    {
        localPlayer = GameManager.instance.GetLocalPlayer();
        this.cardVisuals = cardVisuals;
        this.cardVisuals.Setup(cardData, this);
    }
    public ColorType GetColorType()
    {
        return cardData.color;
    }

    public void TryBuy()
    {
        int price = PriceManager.instance.GetPrice(cardData.color, cardData.rarity);
        if (GameManager.instance.CanAfford(price))
        {
            localPlayer.ModifyGold(-price);
            cardVisuals.SetBought();
        }
    }

    public void Sell()
    {
        localPlayer.ModifyGold(PriceManager.instance.GetPrice(cardData.color, cardData.rarity));
        Destroy(cardVisuals.gameObject);
    }
}
