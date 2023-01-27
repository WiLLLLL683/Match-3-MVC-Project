using System;
using System.Collections.Generic;
using UnityEngine;

namespace Model.Objects
{
    public class CurrencyInventory
    {
        public int Gold { get; private set; }

        public void AddGold(int ammount)
        {
            if (ammount <= 0)
            {
                Debug.LogError("Can't add negative ammount of Gold");
                return;
            }

            Gold += ammount;
        }

        public void TakeGold(int ammount)
        {
            if (ammount <= 0)
            {
                Debug.LogError("Can't remove negative ammount of Gold");
                return;
            }

            if (ammount > Gold)
            {
                Debug.LogError("Not enough Gold");
                return;
            }

            Gold -= ammount;
        }
    }
}