using Config;
using Model.Objects;
using Model.Services;
using NaughtyAttributes;
using System;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Utils
{
    public class CurrencyDebugTool : MonoBehaviour
    {
        public CurrencyType currencyType;
        public int amount = 50;

        private ICurrencyService currencyService;

        [Inject]
        public void Construct(ICurrencyService currencyService)
        {
            this.currencyService = currencyService;
        }

        [Button] public void Add() => currencyService.AddCurrency(currencyType, amount);
        [Button] public void Spend() => currencyService.SpendCurrency(currencyType, amount);
        [Button] public void PrintAmount() => Debug.Log($"You have {currencyService.GetAmount(currencyType)} {currencyType}");
    }
}