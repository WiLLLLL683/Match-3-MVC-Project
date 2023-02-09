using System;
using System.Collections.Generic;
using UnityEngine;

namespace Model.Objects
{
    /// <summary>
    /// Инвентарь для игровых валют
    /// </summary>
    public class CurrencyInventory
    {
        //TODO сделать более универсальным по валютам
        public int Gold { get; private set; }

        /// <summary>
        /// Добавить валюту
        /// </summary>
        public void AddGold(int ammount)
        {
            if (ammount <= 0)
            {
                Debug.LogError("Can't add negative ammount of Gold");
                return;
            }

            Gold += ammount;
        }

        /// <summary>
        /// Забрать валюту из инвентаря
        /// </summary>
        /// <param name="ammount"></param>
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