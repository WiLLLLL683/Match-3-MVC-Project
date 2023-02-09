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
        public Dictionary<CurrencyType, int> currencies;

        public CurrencyInventory()
        {
            currencies = new();
        }

        /// <summary>
        /// For tests only
        /// </summary>
        public CurrencyInventory(CurrencyType type)
        {
            currencies = new();
            currencies.Add(type, 0);
        }

        /// <summary>
        /// Добавить валюту определенного типа
        /// </summary>
        public void AddCurrency(CurrencyType type, int ammount)
        {
            if (ammount <= 0)
            {
                Debug.LogError("Can't add negative ammount of " + type);
                return;
            }

            if (currencies.ContainsKey(type))
            {
                currencies[type] += ammount;
            }
            else
            {
                currencies.Add(type, ammount);
            }
        }

        /// <summary>
        /// Забрать валюту определенного типа из инвентаря
        /// </summary>
        /// <param name="ammount"></param>
        public void TakeCurrency(CurrencyType type, int ammount)
        {
            if (ammount <= 0)
            {
                Debug.LogError("Can't remove negative ammount of " + type);
                return;
            }

            if (!currencies.ContainsKey(type))
            {
                Debug.LogError("You have no " + type);
                return;
            }

            if (ammount > currencies[type])
            {
                Debug.LogError("Not enough " + type);
                return;
            }

            currencies[type] -= ammount;
        }

        public int GetAmmount(CurrencyType type)
        {
            if (!currencies.ContainsKey(type))
            {
                Debug.LogError("You have no " + type);
                return 0;
            }

            return currencies[type];
        }
    }
}