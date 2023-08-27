using Data;
using Model.Factories;
using Model.Objects;
using Model.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UnitTests
{
    public static class TestUtils
    {
        public const int NO_BLOCK = -1;
        public const int DEFAULT_BLOCK = 0;
        public const int RED_BLOCK = 1;
        public const int BLUE_BLOCK = 2;
        public const int GREEN_BLOCK = 3;
        public const int YELLOW_BLOCK = 4;

        public static IBlockType DefaultBlockType = CreateBlockType(DEFAULT_BLOCK);
        public static IBlockType RedBlockType = CreateBlockType(RED_BLOCK);
        public static IBlockType BlueBlockType = CreateBlockType(BLUE_BLOCK);
        public static IBlockType GreenBlockType = CreateBlockType(GREEN_BLOCK);
        public static IBlockType YellowBlockType = CreateBlockType(YELLOW_BLOCK);
        public static ICellType BasicCellType = CreateCellType(true, true);
        public static ICellType InvisibleCellType = CreateCellType(false, true);
        public static ICellType NotPlayableCellType = CreateCellType(false, false);

        /// <summary>
        /// Создать дефолтный уровень со всеми компонентами
        /// Указать размеры игрового поля
        /// </summary>
        public static Level CreateLevel(int xLength, int yLength)
        {
            var goals = new Counter[1] { new Counter(DefaultBlockType, 2) };
            var restrictions = new Counter[1] { new Counter(new Turn(), 2) };
            var matchPatterns = new Pattern[1] { new Pattern(new bool[1, 1] { { true } })  };
            var hintPatterns = new HintPattern[1] { new HintPattern(new bool[1, 1] { { true } }, new(0, 0), Directions.Up) };

            return new Level
            {
                gameBoard = CreateGameBoard(xLength, yLength),
                goals = goals,
                restrictions = restrictions,
                matchPatterns = matchPatterns,
                hintPatterns = hintPatterns,
                balance = CreateBalance(DEFAULT_BLOCK, RED_BLOCK)
            };
        }

        /// <summary>
        /// Создать дефолтный уровень со всеми компонентами
        /// Указать размеры игрового поля и паттерны для матча
        /// </summary>
        public static Level CreateLevel(int xLength, int yLength, Pattern[] matchPatterns)
        {
            Level level = CreateLevel(xLength, yLength);
            level.matchPatterns = matchPatterns;
            return level;
        }

        /// <summary>
        /// Указать размеры и типы блоков построчно
        /// </summary>
        public static Balance CreateBalance(params int[] typeIds)
        {
            return new Balance
            {
                typesWeight = CreateListOfWeights(typeIds),
                defaultBlockType = new() { blockType = DefaultBlockType }
            };
        }

        /// <summary>
        /// Создать инвентарь с 1 валютой заданного типа
        /// </summary>
        public static CurrencyInventory CreateCurrencyInventory(CurrencyType type)
        {
            CurrencyInventory inventory = new();
            inventory.currencies.Add(type, 0);
            return inventory;
        }

        /// <summary>
        /// Указать размеры и типы блоков построчно
        /// </summary>
        public static GameBoard CreateGameBoard(int xLength, int yLength, params int[] typeIds)
        {
            GameBoard gameBoard = new();

            //заполнить игровое поле клетками
            gameBoard.cells = new Cell[xLength, yLength];
            gameBoard.blocks = new List<Block>();

            for (int y = 0; y < yLength; y++)
            {
                for (int x = 0; x < xLength; x++)
                {
                    gameBoard.cells[x, y] = new Cell(new BasicCellType(), new Vector2Int(x, y));
                }
            }

            //Преобразовать 2д массив клеток в 1д построчно
            List<Cell> cells = new();
            for (int y = 0; y < gameBoard.cells.GetLength(1); y++)
            {
                for (int x = 0; x < gameBoard.cells.GetLength(0); x++)
                {
                    cells.Add(gameBoard.cells[x,y]);
                }
            }

            //заспавнить блоки из параметров
            for (int i = 0; i < cells.Count && i < typeIds.Length; i++)
            {
                if (typeIds[i] == NO_BLOCK)
                {
                    continue;
                }
                IBlockType blockType = CreateBlockType(typeIds[i]);
                Block block = cells[i].SpawnBlock(blockType);
                gameBoard.RegisterBlock(block);
            }

            return gameBoard;
        }

        /// <summary>
        /// Указать размеры и значения паттерна построчно
        /// </summary>
        public static Pattern CreatePattern(int xLength, int yLength, params bool[] values)
        {
            bool[,] grid = new bool[xLength, yLength];

            int i = 0;

            for (int y = 0; y < yLength || i < values.Length; y++)
            {
                for (int x = 0; x < xLength || i < values.Length; x++)
                {
                    grid[x, y] = values[i];
                    i = (y * xLength) + x + 1;
                }
            }

            return new Pattern(grid);
        }

        /// <summary>
        /// Создание блока указанного типа
        /// </summary>
        public static Block CreateBlock(int typeId, Cell cell) => new Block(CreateBlockType(typeId), cell);

        private static IBlockType CreateBlockType(int typeId) => new BasicBlockType(typeId);

        private static ICellType CreateCellType(bool isPlayable, bool canContainBlock = true) => new BasicCellType(isPlayable, canContainBlock);

        private static List<BlockType_Weight> CreateListOfWeights(params int[] typeIds)
        {
            List<BlockType_Weight> list = new();
            for (int i = 0; i < typeIds.Length; i++)
            {
                list.Add(CreateBlockTypeWeight(typeIds[i], 100));
            }
            return list;
        }

        private static BlockType_Weight CreateBlockTypeWeight(int typeId, int weight) => new BlockType_Weight(CreateBlockType(typeId), weight);
    }
}