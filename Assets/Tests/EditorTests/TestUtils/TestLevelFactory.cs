using System.Collections.Generic;
using UnityEngine;
using Model.Objects;
using Model.Services;

namespace TestUtils
{
    public static class TestLevelFactory
    {
        /// <summary>
        /// Создать дефолтный уровень со всеми компонентами
        /// Указать размеры игрового поля
        /// </summary>
        public static Level CreateLevel(int xLength, int yLength)
        {
            var goals = new Counter[1] { new Counter(TestBlockFactory.DefaultBlockType, 2) };
            var restrictions = new Counter[1] { new Counter(new Turn(), 2) };
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
        public static GameBoard CreateGameBoard(int xLength, int yLength, int rowsOfInvisibleCells, params int[] typeIds)
        {
            GameBoard gameBoard = new(new Cell[xLength, yLength], rowsOfInvisibleCells);

            //заполнить игровое поле клетками
            for (int y = 0; y < yLength; y++)
            {
                for (int x = 0; x < xLength; x++)
                {
                    gameBoard.Cells[x, y] = new Cell(new BasicCellType(), new Vector2Int(x, y));
                }
            }

            //Преобразовать 2д массив клеток в 1д построчно
            List<Cell> cells = new();
            for (int y = 0; y < gameBoard.Cells.GetLength(1); y++)
            {
                for (int x = 0; x < gameBoard.Cells.GetLength(0); x++)
                {
                    cells.Add(gameBoard.Cells[x, y]);
                }
            }

            //заспавнить блоки из параметров
            for (int i = 0; i < cells.Count && i < typeIds.Length; i++)
            {
                if (typeIds[i] == TestBlockFactory.NO_BLOCK)
                {
                    continue;
                }
                Block block = TestBlockFactory.CreateBlockInCell(typeIds[i], cells[i]);
                gameBoard.RegisterBlock(block);
            }

            return gameBoard;
        }
    }
}