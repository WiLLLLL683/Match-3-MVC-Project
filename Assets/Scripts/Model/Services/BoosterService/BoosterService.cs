﻿using System;
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

        private readonly Game model;
        private readonly IBoosterFactory factory;
        private readonly IValidationService validationService;
        private readonly IBlockMoveService moveService;
        private readonly IBlockDestroyService destroyService;

        private Dictionary<int, int> Boosters => model.BoosterInventory.boosters;

        public BoosterService(Game game,
            IBoosterFactory factory,
            IValidationService validationService,
            IBlockMoveService moveService,
            IBlockDestroyService destroyService)
        {
            this.model = game;
            this.factory = factory;
            this.validationService = validationService;
            this.moveService = moveService;
            this.destroyService = destroyService;
        }

        public void AddBooster(int id, int ammount)
        {
            if (!IsValidAmount(ammount))
                return;

            if (Boosters.ContainsKey(id))
            {
                Boosters[id] += ammount;
            }
            else
            {
                Boosters.Add(id, ammount);
            }

            OnAmountChanged?.Invoke(id, Boosters[id]);
        }

        public void RemoveBooster(int id, int ammount)
        {
            if (!IsValidAmount(ammount))
                return;

            if (!IsAvaliable(id))
                return;

            Boosters[id] -= ammount;
            Boosters[id] = Mathf.Max(0, Boosters[id]);

            OnAmountChanged?.Invoke(id, Boosters[id]);
        }

        public void UseBooster(int id, Vector2Int startPosition)
        {
            if (!IsAvaliable(id))
                return;

            IBooster booster = factory.Create(id);
            booster.Execute(startPosition, destroyService, moveService);

            RemoveBooster(id, 1);
        }

        public int GetBoosterAmount(int id)
        {
            if (!IsAvaliable(id))
                return 0;

            return Boosters[id];
        }

        private bool IsAvaliable(int id)
        {
            bool isAvaliable = Boosters.ContainsKey(id) && Boosters[id] > 0;

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
                Debug.LogWarning("Can't add or remove negative ammount of Boosters");
            }

            return isValidAmount;
        }
    }
}