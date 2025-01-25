using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Random = System.Random;

public static class Helpers 
{
    public static void Shuffle<T>(this IList<T> list)
    {
        Random rng = new Random();
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    public static Color GetColor(ColorType colorType)
    {
        switch(colorType)
        {
            case ColorType.Red:
                return new Color(0.6392157f, 0.0986509f, 0.04307583f);
            case ColorType.Purple:
                return new Color(0.3402972f, 0.07195622f, 0.4622642f);
            case ColorType.Yellow:
                return new Color(1f, 0.7808987f, 0.2783019f);
            default:
                return Color.white;
        }
    }

    public static int GetPriceTierC(int level)
    {
        switch(level)
        {
            case 1: return 1;
            case 2: return 3;
            case 3:return 5;
            case 4: return 7;
            case 5:return 9;
            default: return 1;
        }
    }
    public static int GetPriceTierB(int level)
    {
        switch (level)
        {
            case 1: return 3;
            case 2: return 5;
            case 3: return 7;
            case 4: return 10;
            case 5: return 13;
            default: return 1;
        }
    }
    public static int GetPriceTierA(int level)
    {
        switch (level)
        {
            case 1: return 15;
            case 2: return 15;
            case 3: return 20;
            case 4: return 26;
            case 5: return 33;
            default: return 1;
        }
    }
}
