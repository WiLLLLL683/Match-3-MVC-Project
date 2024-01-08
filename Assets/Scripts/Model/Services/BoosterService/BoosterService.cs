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
        public event Action<int, int> OnAmountChanged;

        private readonly BoosterInventory inventory;
        private readonly IBoosterFactory factory;
        private readonly IBlockDestroyService destroyService;
        private readonly IValidationService validationService;

        public BoosterService(Game game,
            IBoosterFactory factory,
            IBlockDestroyService destroyService,
            IValidationService validationService)
        {
            this.inventory = game.BoosterInventory;
            this.factory = factory;
            this.destroyService = destroyService;
            this.validationService = validationService;
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

            OnAmountChanged?.Invoke(id, inventory.boosters[id]);
        }

        public void RemoveBooster(int id, int ammount)
        {
            if (!IsValidAmount(ammount))
                return;

            if (!IsAvaliable(id))
                return;

            inventory.boosters[id] -= ammount;
            inventory.boosters[id] = Mathf.Max(0, inventory.boosters[id]);

            OnAmountChanged?.Invoke(id, inventory.boosters[id]);
        }

        public bool UseBooster(int id, Vector2Int startPosition)
        {
            if (!IsAvaliable(id))
                return false;

            IBooster booster = factory.Create(id);
            bool success = booster.Execute(startPosition, destroyService, validationService);

            if (!success)
                return false;

            RemoveBooster(id, 1);
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