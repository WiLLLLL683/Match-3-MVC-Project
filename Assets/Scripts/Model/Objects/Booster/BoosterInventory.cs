using System;
using System.Collections.Generic;

namespace Model.Objects
{
    public class BoosterInventory
    {
        /// <summary>
        /// Key = Id, value = amount
        /// </summary>
        public Dictionary<int, int> boosters = new();
    }
}