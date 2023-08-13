using Data;
using Model.Objects;
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

        public static IBlockType DefaultBlockType = CreateBasicBlockType(DEFAULT_BLOCK);
        public static ICellType BasicCellType = CreateCellType(true, true);
        public static ICellType NotPlayableCellType = CreateCellType(false, false);

        /// <summary>
        /// Указать размеры и типы блоков построчно
        /// </summary>
        public static List<BlockType_Weight> CreateListOfWeights(params int[] typeIds)
        {
            List<BlockType_Weight> list = new();
            for (int i = 0; i < typeIds.Length; i++)
            {
                list.Add(CreateBlockTypeWeight(typeIds[i], 100));
            }
            return list;
        }

        /// <summary>
        /// Указать размеры и типы блоков построчно
        /// </summary>
        public static GameBoard CreateGameBoard(int xLength, int yLength, params int[] typeIds)
        {
            GameBoard gameBoard = new GameBoard(xLength, yLength);

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
                var blockType = CreateBasicBlockType(typeIds[i]);
                var block = cells[i].SpawnBlock(blockType);
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



        private static ICellType CreateCellType(bool isPlayable, bool canContainBlock = true) => new BasicCellType(isPlayable, canContainBlock);
        private static IBlockType CreateBasicBlockType(int typeId) => new BasicBlockType(typeId);
        private static BlockType_Weight CreateBlockTypeWeight(int typeId, int weight) => new BlockType_Weight(CreateBasicBlockType(typeId), weight);
    }
}