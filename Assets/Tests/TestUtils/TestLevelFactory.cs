using Model.Objects;
using Model.Services;
using System.Collections.Generic;
using UnityEngine;

namespace TestUtils
{
    public static class TestLevelFactory
    {
        /// <summary>
        /// Создать игру с дефолтным уровенем со всеми компонентами (кроме стейт-машины)
        /// Указать размеры игрового поля
        /// </summary>
        public static Game CreateGame(int xLength, int yLength)
        {
            var levelProgress = new LevelProgress();
            var playerSettings = new PlayerSettings();
            var currencyInventory = new CurrencyInventory();
            var game = new Game(levelProgress, playerSettings, currencyInventory);
            game.CurrentLevel = CreateLevel(xLength, yLength);

            return game;
        }

        /// <summary>
        /// Создать дефолтный уровень со всеми компонентами
        /// Указать размеры игрового поля
        /// </summary>
        public static Level CreateLevel(int xLength, int yLength)
        {
            var goals = new Counter[1] { new Counter(TestBlockFactory.DefaultBlockType, 2) };
            var restrictions = new Counter[1] { new Counter(new Turn(-100), 2) };
            var hintPatterns = new HintPattern[1] { TestPatternFactory.CreateDefaultHintPattern() };
            var matchPatterns = new MatchPattern[1] { new MatchPattern(new bool[1, 1] { { true } }, hintPatterns) };

            return new Level
            {
                gameBoard = CreateGameBoard(xLength, yLength, 0),
                goals = goals,
                restrictions = restrictions,
                matchPatterns = matchPatterns,
            };
        }

        /// <summary>
        /// Создать дефолтный уровень со всеми компонентами
        /// Указать размеры игрового поля и паттерны для матча
        /// </summary>
        public static Level CreateLevel(int xLength, int yLength, MatchPattern[] matchPatterns)
        {
            Level level = CreateLevel(xLength, yLength);
            level.matchPatterns = matchPatterns;
            return level;
        }


        /// <summary>
        /// Указать размеры и типы блоков построчно
        /// </summary>
        public static GameBoard CreateGameBoard(int xLength, int yLength, int rowsOfInvisibleCells)
        {
            int hiddenRowsStartIndex = yLength - rowsOfInvisibleCells;
            GameBoard gameBoard = new(new Cell[xLength, yLength], hiddenRowsStartIndex);

            //заполнить игровое поле клетками
            for (int y = 0; y < yLength; y++)
            {
                for (int x = 0; x < xLength; x++)
                {
                    gameBoard.Cells[x, y] = new Cell(new BasicCellType(), new Vector2Int(x, y));
                }
            }

            return gameBoard;
        }
    }
}