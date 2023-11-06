using System;
using UnityEngine;
using Config;
using Model.Factories;
using Utils;
using Model.Infrastructure;

namespace Model.Objects
{
    /// <summary>
    /// ����� ����� ��� ������ ����
    /// ������ ������� ������ � ������� ���������� ����
    /// ���������� ������ ���-����
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