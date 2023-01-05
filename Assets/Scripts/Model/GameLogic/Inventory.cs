using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    public int Gold { get; private set; }

    public void AddGold(int ammount)
    {
        if (ammount > 0)
        {
            Gold += ammount;
        }
        else
        {
            Debug.LogError("Can't add negative ammount of Gold");
        }
    }

    public void RemoveGold(int ammount)
    {
        if (ammount > 0)
        {
            Gold -= ammount;
        }
        else
        {
            Debug.LogError("Can't remove negative ammount of Gold");
        }
    }
}