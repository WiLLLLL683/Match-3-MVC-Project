using System;
using System.Collections.Generic;
using UnityEngine;

namespace Model.Objects
{
    public class CurrencyInventory
    {
        public Dictionary<CurrencyType, int> currencies = new();
    }
}