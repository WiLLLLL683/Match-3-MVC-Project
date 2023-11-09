using Model.Objects;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Model.Services
{
    [Serializable]
    public class CurrencyService : ICurrencyService
    {
        public event Action<CurrencyType, int> OnChange;

        private readonly CurrencyInventory inventory;

        public CurrencyService(CurrencyInventory inventory) => this.inventory = inventory;

        public void AddCurrency(CurrencyType type, int ammount)
        {
            if (ammount < 0)
            {
                Debug.LogError("Can't add negative ammount of " + type);
                return;
            }

            if (inventory.currencies.ContainsKey(type))
            {
                inventory.currencies[type] += ammount;
            }
            else
            {
                inventory.currencies.Add(type, ammount);
            }

            OnChange?.Invoke(type, GetAmount(type));
        }

        public void SpendCurrency(CurrencyType type, int ammount)
        {
            if (ammount < 0)
            {
                Debug.LogError("Can't remove negative ammount of " + type);
                return;
            }

            if (!inventory.currencies.ContainsKey(type))
            {
                Debug.LogError("You have no " + type);
                return;
            }

            if (ammount > inventory.currencies[type])
            {
                Debug.LogError("Not enough " + type);
                return;
            }

            inventory.currencies[type] -= ammount;
            OnChange?.Invoke(type, GetAmount(type));
        }

        public int GetAmount(CurrencyType type)
        {
            if (!inventory.currencies.ContainsKey(type))
            {
                Debug.LogError("You have no " + type);
                return 0;
            }

            return inventory.currencies[type];
        }

        public void ClearAllCurrencies() => inventory.currencies.Clear();
    }
}