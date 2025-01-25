using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardVisuals : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private TMP_Text title, price, rarityText;
    [SerializeField] private Image preview, rarityImage;
    [SerializeField] private GameObject overlay, buyBtn, sellBtn;
    private Card parent;
    [SerializeField] private Transform followPoint;
    private float lerpSpeed = 5;

    public void Setup(CardData cardData, Card card)
    {
        parent = card;
        title.text = cardData.Name;
        price.text ="GOLD: " +PriceManager.instance.GetPrice(cardData.color, cardData.rarity).ToString();
        rarityText.text = cardData.rarity.ToString();
        preview.sprite = cardData.preview;
        rarityImage.color = Helpers.GetColor(cardData.color);
        overlay.SetActive(false);
        buyBtn.SetActive(true);
        sellBtn.SetActive(false);
    }
    public Transform GetFollowPoint()
    {
        return followPoint;
    }
    private void Update()
    {
        if (followPoint == null)
            Destroy(gameObject);
        else
            transform.position = Vector3.Lerp(transform.position, followPoint.position, Time.deltaTime * lerpSpeed);
    }
    public void Buy()
    {
        parent.TryBuy();
    }

    public void Sell()
    {
        parent.Sell();
    }  

    public void SetBought()
    {
        followPoint.SetParent(BoardManager.instance.hand);
        overlay.SetActive(false);
        buyBtn.SetActive(false);
        sellBtn.SetActive(true);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        overlay.SetActive(true);   
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        overlay.SetActive(false);
    }
}
