using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private Deck deck;
    [SerializeField] private Transform currentMarketParent, nextMarketParent, eventCardPoint;
    // Start is called before the first frame update
    private int cardsInMarket = 5;
    [SerializeField] private CardVisuals cardVisuals;
    [SerializeField] private EventCardVisuals eventCardVisuals;
    [SerializeField] private Transform cardParent;
    private Player localPlayer;
    public static GameManager instance;
    private List<Card> nextMarket;
    private List<CountType> countTypes = new List<CountType>();
    [SerializeField] int roundNumber = 0;
    [SerializeField] private float[] crashChance;

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
        PlayEventCard();

        //update price visuals
        foreach(CardVisuals visuals in cardParent.GetComponentsInChildren<CardVisuals>())
        {
            visuals.UpdatePrice();
        }
        //generate next market
        GenerateNextMarket();
        //update crash chance
        BoardManager.instance.UpdateCrashChance(crashChance[roundNumber]);
        if (crashChance[roundNumber] > Random.value)
            BoardManager.instance.EnableLoseMenu();
        roundNumber++;
    }

    public void PlayEventCard()
    {
        //Setup visuals
        EventCard eventCard = deck.GetCardFromEventDeck(); 
        EventCardVisuals visuals = Instantiate(eventCardVisuals, cardParent);
        visuals.GetFollowPoint().SetParent(eventCardPoint);
        visuals.GetFollowPoint().transform.localPosition = Vector3.zero;
        visuals.transform.SetAsLastSibling();
        visuals.Setup(eventCard.GetEventCardData());
        // resolve effect
        switch(eventCard.GetEventCardData().cardType) 
        {
            case EventCardType.Yellow:
                PriceManager.instance.TogglePriceTier(1, ColorType.Yellow);
                break;
            case EventCardType.Purple:
                PriceManager.instance.TogglePriceTier(1, ColorType.Purple);
                break;
            case EventCardType.Red:
                PriceManager.instance.TogglePriceTier(1, ColorType.Red);
                break;
            default:
                break;
        }
    }

    public bool CanAfford(int cost)
    {
        return localPlayer.GetGold() >= cost;            
    }
    public Player GetLocalPlayer()
    {
        return localPlayer;
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Reset()
    {
        SceneManager.LoadScene(1);
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
        localPlayer.ModifyGold(0);
        GenerateNextMarket();
        UpdateMarket();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
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
