﻿using System;
using System.Collections.Generic;
using UnityEngine;
using Model.Objects;

namespace Model.Services
{
    [Serializable]
    public class BoosterService : IBoosterService
    {
        private readonly Dictionary<Type, int> boosters = new();

        public void AddBooster<T>(int ammount) where T : IBooster
        {
            if (ammount <= 0)
            {
                Debug.LogError("Can't add negative ammount of Boosters");
                return;
            }

            Type boosterType = typeof(T);
            if (boosters.ContainsKey(boosterType))
            {
                boosters[boosterType] += ammount;
            }
            else
            {
                boosters.Add(boosterType, ammount);
            }
        }

        public IBooster SpendBooster<T>() where T : IBooster, new()
        {
            Type boosterType = typeof(T);
            if (!boosters.ContainsKey(boosterType))
            {
                Debug.LogError("You have no booster of this type");
                return null;
            }

            boosters[boosterType]--;
            if (boosters[boosterType] <= 0)
            {
                boosters.Remove(boosterType);
            }

            return new T();
        }

        public int GetBoosterAmount<T>() where T : IBooster
        {
            Type boosterType = typeof(T);
            if (boosters.ContainsKey(boosterType))
            {
                return boosters[boosterType];
            }
            else
            {
                return 0;
            }
        }
    }
}