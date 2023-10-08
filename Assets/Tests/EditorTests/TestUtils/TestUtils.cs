using System.Collections.Generic;
using UnityEngine;
using Model.Objects;
using Model.Services;

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

        public static BasicBlockType DefaultBlockType = CreateBlockType(DEFAULT_BLOCK);
        public static BasicBlockType RedBlockType = CreateBlockType(RED_BLOCK);
        public static BasicBlockType BlueBlockType = CreateBlockType(BLUE_BLOCK);
        public static BasicBlockType GreenBlockType = CreateBlockType(GREEN_BLOCK);
        public static BasicBlockType YellowBlockType = CreateBlockType(YELLOW_BLOCK);
        public static CellType BasicCellType = CreateCellType(true, true);
        public static CellType InvisibleCellType = CreateCellType(false, true);
        public static CellType NotPlayableCellType = CreateCellType(false, false);

        /// <summary>
        /// Создать дефолтный уровень со всеми компонентами
        /// Указать размеры игрового поля
        /// </summary>
        public static Level CreateLevel(int xLength, int yLength)
        {
            var goals = new Counter[1] { new Counter(DefaultBlockType, 2) };
            var restrictions = new Counter[1] { new Counter(new Model.Services.Turn(), 2) };
            var matchPatterns = new Pattern[1] { new Pattern(new bool[1, 1] { { true } })  };
            var hintPatterns = new HintPattern[1] { new HintPattern(new bool[1, 1] { { true } }, new(0, 0), Directions.Up) };

            return new Level
            {
                gameBoard = CreateGameBoard(xLength, yLength, 0),
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
            var balance = new Balance();
            balance.SetWeights(CreateListOfWeights(typeIds));
            balance.defaultBlockType = DefaultBlockType;
            return balance;
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
                    cells.Add(gameBoard.Cells[x,y]);
                }
            }

            //заспавнить блоки из параметров
            for (int i = 0; i < cells.Count && i < typeIds.Length; i++)
            {
                if (typeIds[i] == NO_BLOCK)
                {
                    continue;
                }
                Block block = CreateBlockInCell(typeIds[i], cells[i]);
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
        public static Block CreateBlock(int typeId, Vector2Int position = default)
        {
            var type = CreateBlockType(typeId);
            return new Block(type, position);
        }

        /// <summary>
        /// Создание блока указанного типа
        /// </summary>
        public static Block CreateBlock(BlockType type, Vector2Int position = default)
        {
            return new Block(type, position);
        }

        /// <summary>
        /// Создание блока указанного типа
        /// </summary>
        public static Block CreateBlockInCell(int typeId, Cell cell)
        {
            var type = CreateBlockType(typeId);
            var block = new Block(type, cell.Position);
            cell.SetBlock(block);
            return block;
        }

        public static Pattern DotPattern1x1()
        {
            bool[,] grid = new bool[1, 1];
            grid[0, 0] = true;
            return new Pattern(grid);
        }

        public static Pattern VertLinePattern1x3()
        {
            bool[,] grid = new bool[1, 3];
            grid[0, 0] = true;
            grid[0, 1] = true;
            grid[0, 2] = true;
            return new Pattern(grid);
        }

        public static Pattern CrossPattern3x3()
        {
            bool[,] grid = new bool[3, 3];
            grid[0, 1] = true;
            grid[1, 0] = true;
            grid[1, 1] = true;
            grid[1, 2] = true;
            grid[2, 1] = true;
            var pattern = new Pattern(grid);
            pattern.originPosition = new(0, 1);
            return pattern;
        }

        private static BasicBlockType CreateBlockType(int typeId) => new BasicBlockType(typeId);

        private static CellType CreateCellType(bool isPlayable, bool canContainBlock = true) => new BasicCellType(isPlayable, canContainBlock);

        private static List<BlockType_Weight> CreateListOfWeights(params int[] typeIds)
        {
            List<BlockType_Weight> list = new();
            for (int i = 0; i < typeIds.Length; i++)
            {
                list.Add(new(CreateBlockType(typeIds[i]), 100));
            }
            return list;
        }
    }
}