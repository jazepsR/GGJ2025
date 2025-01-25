using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Deck deck;
    [SerializeField] private Transform currentMarketParent, nextMarketParent;
    // Start is called before the first frame update
    private int cardsInMarket = 5;
    [SerializeField] private CardVisuals cardVisuals;
    [SerializeField] private Transform cardParent;
    private Player localPlayer;
    public static GameManager instance;
    private List<Card> nextMarket;
    private List<CountType> countTypes = new List<CountType>();

    void Awake()
    {
        if(instance!= null)
            Destroy(instance);  
        instance = this;
        deck = GetComponent<Deck>();
        localPlayer = new Player(15, "Player 1", true);
    }

    private void SetupCountTypes()
    {
        countTypes.Clear();
        countTypes.Add(new CountType(ColorType.Yellow, 0));
        countTypes.Add(new CountType(ColorType.Purple, 0));
        countTypes.Add(new CountType(ColorType.Red, 0));
    }
    public void UpdateMarket()
    {
        //clear current market
        for (int i = currentMarketParent.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(currentMarketParent.transform.GetChild(i).gameObject);
        }
        SetupCountTypes();
        //Calculate price changes based on supply of tulips in next market
        foreach (Card card in nextMarket)
        {
            ColorType colorType = card.GetColorType();
            bool elementFound = false;
            foreach(CountType type in countTypes)
            {
                if(type.colorType == colorType)
                {
                    type.count++;
                    elementFound = true;
                    break;
                }
            }
            if (!elementFound)
                countTypes.Add(new CountType(colorType, 1));
        }
        PriceManager.instance.UpdatePrices(countTypes);

        //move cards from next shipment to current shipment
        //Debug.LogError("next market card count: " + nextMarketParent.transform.childCount);
        for (int i = nextMarketParent.transform.childCount - 1; i >= 0; i--)
        {
            Transform card = nextMarketParent.transform.GetChild(i);
            card.transform.SetParent(currentMarketParent);
            card.SetAsFirstSibling();
        }
        
        //generate next market
        GenerateNextMarket();
    }
    public bool CanAfford(int cost)
    {
        return localPlayer.GetGold() >= cost;            
    }
    public Player GetLocalPlayer()
    {
        return localPlayer;
    }

    public void GenerateNextMarket()
    {
        nextMarket = new();
        for(int i=0;i<cardsInMarket;i++)
        {
            Card nextCard= deck.GetCardFromTulipDeck();
            nextMarket.Add(nextCard);
            CardVisuals visuals = Instantiate(cardVisuals, cardParent);
            visuals.GetFollowPoint().SetParent(nextMarketParent);
            visuals.transform.SetAsFirstSibling();
            nextCard.SetupCardVisuals(visuals);
        }
    }

    private void Start()
    {
        GenerateNextMarket();
        UpdateMarket();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public class CountType
{
    public CountType(ColorType colorType, int count)
    {
        this.colorType = colorType;
        this.count = count;
    }
    public ColorType colorType;
    public int count;
}
