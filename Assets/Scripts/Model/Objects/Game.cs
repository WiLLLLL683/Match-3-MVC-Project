using System;

namespace Model.Objects
{
    /// <summary>
    /// Корневой объект модели игры
    /// Основная зависимость для сервисов и презентеров
    /// </summary>
    [Serializable]
    public class Game
    {
        //meta game
        public LevelProgress LevelProgress;
        public PlayerSettings PlayerSettings;
        public CurrencyInventory CurrencyInventory;
        public BoosterInventory BoosterInventory;

        //core game
        public Level CurrentLevel;

        public Game(LevelProgress levelProgress,
            PlayerSettings playerSettings,
            CurrencyInventory currencyInventory)
        {
            LevelProgress = levelProgress;
            PlayerSettings = playerSettings;
            CurrencyInventory = currencyInventory;
        }
    }
}