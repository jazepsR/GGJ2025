using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    [SerializeField] private TMP_Text goldText, endText, crashChanceText;
    public GameObject endMenu;
    public static BoardManager instance;
    public Transform hand;
    private void Awake()
    {
        if (instance != null)
            Destroy(instance);
        instance = this;
    }
    // Start is called before the first frame update
    public void UpdateGold(int gold)
    {
        goldText.text = "GOLD: " + gold;
    }

    public void EnableLoseMenu()
    {
        endMenu.SetActive(true);
        endText.text = "Thank you for playing!\r\nyou earned " + GameManager.instance.GetLocalPlayer().GetGold() + " gold";
    }

    public void UpdateCrashChance(float crashChance)
    {
        crashChanceText.text = "Crash chance: " + (crashChance * 100) + "%";
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
