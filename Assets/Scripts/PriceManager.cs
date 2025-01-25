using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class PriceManager : MonoBehaviour
{
    public static PriceManager instance;
    int redLevel = 1;
    int yellowLevel = 1;
    int purpleLevel = 1;
    [SerializeField] private Transform redTierIndicator, yellowTierIndicator, purpTierIndicator;
    [SerializeField] private Transform[] tierParents;
    public void Awake()
    {
        if(instance != null)
            Destroy(instance);
        instance = this;
    }
    public void UpdatePrices(List<CountType> countTypes)
    {
        int toAdd = 1;
        List<CountType> sortedTypes = countTypes.OrderBy(o => o.count).ToList();
        //sortedTypes.Reverse();
        for (int i=0;i< sortedTypes.Count;i++)
        {
            GetColorLevelRef(sortedTypes[i].colorType) = GetColorLevel(sortedTypes[i].colorType) + toAdd;
            if (i != sortedTypes.Count - 1 && sortedTypes[i + 1] != sortedTypes[i])
                toAdd--;
        }
        /*foreach (CountType kvp in countTypes)
        {
            GetColorLevelRef(kvp.colorType) = GetColorLevel(kvp.colorType) + toAdd;
           // Debug.Log(kvp.Key + "  " + GetColorLevel(kvp.Key));
            toAdd++;
        }*/
        redLevel = Math.Clamp(redLevel, 1, 5);
        yellowLevel = Math.Clamp(yellowLevel, 1, 5);
        purpleLevel = Math.Clamp(purpleLevel, 1, 5);
        Debug.Log("red " + redLevel+ " yellow " + yellowLevel + " purple " + purpleLevel);
        SetupTierIndicators();
    }

    public void SetupTierIndicators()
    {
        redTierIndicator.position = tierParents[redLevel-1].position;
        yellowTierIndicator.position = tierParents[yellowLevel-1].position;
        purpTierIndicator.position = tierParents[purpleLevel - 1].position;
    }
    public int GetPrice(ColorType colorType, Rarity rarity)
    {
        switch(rarity)
        {
            case Rarity.I:
                return Helpers.GetPriceTierA(GetColorLevel(colorType));
            case Rarity.II:
                return Helpers.GetPriceTierB(GetColorLevel(colorType));
            case Rarity.III:
                return Helpers.GetPriceTierC(GetColorLevel(colorType));
            default:
                return 0;
        }
    }
    private ref int GetColorLevelRef(ColorType colorType)
    {
        switch (colorType)
        {
            case ColorType.Red:
                return ref redLevel;
            case ColorType.Yellow:
                return ref yellowLevel;
            case ColorType.Purple:
                return ref purpleLevel;
        }
        return ref purpleLevel;
    }
    private int GetColorLevel(ColorType colorType)
    {
       switch(colorType)
        {
            case ColorType.Red:
                return redLevel;
            case ColorType.Yellow:
                return yellowLevel;
            case ColorType.Purple:
                return purpleLevel;
            default: return 1;
        }
    }
}
