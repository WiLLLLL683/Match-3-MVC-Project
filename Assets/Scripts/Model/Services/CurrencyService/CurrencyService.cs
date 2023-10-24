﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace Model.Services
{
    [Serializable]
    public class CurrencyService : ICurrencyService
    {
        private readonly Dictionary<CurrencyType, int> currencies;

        public CurrencyService(Dictionary<CurrencyType, int> currencies)
        {
            this.currencies = currencies;
        }

        public CurrencyService()
        {
            currencies = new(); //TODO загрузка из сохранения
        }

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

        public void SpendCurrency(CurrencyType type, int ammount)
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

        public int GetAmount(CurrencyType type)
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