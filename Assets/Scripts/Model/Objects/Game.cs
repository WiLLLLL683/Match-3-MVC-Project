using System;
using UnityEngine;
using Config;
using Model.Factories;
using Utils;
using Model.Infrastructure;

namespace Model.Objects
{
    /// <summary>
    /// Точка входа для модели игры
    /// Хранит объекты модели с текущим состоянием игры
    /// Определяет стейты кор-игры
    /// </summary>
    [Serializable]
    public class Game
    {
        //meta game
        public LevelProgress LevelProgress;
        public PlayerSettings PlayerSettings;
        public CurrencyInventory CurrencyInventory;

        //core game
        public Level CurrentLevel;

        public Game(
            LevelProgress levelProgress,
            PlayerSettings playerSettings,
            CurrencyInventory currencyInventory)
        {
            LevelProgress = levelProgress;
            PlayerSettings = playerSettings;
            CurrencyInventory = currencyInventory;
        }
    }
}