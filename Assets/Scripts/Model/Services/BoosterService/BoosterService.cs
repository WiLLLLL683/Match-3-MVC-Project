using System;
using System.Collections.Generic;
using UnityEngine;
using Model.Objects;
using Model.Factories;

namespace Model.Services
{
    [Serializable]
    public class BoosterService : IBoosterService
    {
        private readonly BoosterInventory inventory;
        private readonly IBoosterFactory factory;

        public BoosterService(Game game, IBoosterFactory factory)
        {
            this.inventory = game.BoosterInventory;
            this.factory = factory;
        }

        public void AddBooster(int id, int ammount)
        {
            if (!IsValidAmount(ammount))
                return;

            if (inventory.boosters.ContainsKey(id))
            {
                inventory.boosters[id] += ammount;
            }
            else
            {
                inventory.boosters.Add(id, ammount);
            }
        }

        public void RemoveBooster(int id, int ammount)
        {
            if (!IsValidAmount(ammount))
                return;

            if (!IsAvaliable(id))
                return;

            inventory.boosters[id] -= ammount;

            if (inventory.boosters[id] <= 0)
            {
                inventory.boosters.Remove(id);
            }
        }

        public bool UseBooster(int id)
        {
            if (!IsAvaliable(id))
                return false;

            inventory.boosters[id]--;
            IBooster booster = factory.Create(id);
            booster.Execute();
            return true;
        }

        public int GetBoosterAmount(int id)
        {
            if (!IsAvaliable(id))
                return 0;

            return inventory.boosters[id];
        }

        private bool IsAvaliable(int id)
        {
            bool isAvaliable = inventory.boosters.ContainsKey(id) && inventory.boosters[id] > 0;

            if (!isAvaliable)
            {
                Debug.LogWarning("You have no booster of this type");
            }

            return isAvaliable;
        }

        private bool IsValidAmount(int ammount)
        {
            bool isValidAmount = ammount > 0;

            if (!isValidAmount)
            {
                Debug.LogWarning("Can't add negative ammount of Boosters");
            }

            return isValidAmount;
        }
    }
}