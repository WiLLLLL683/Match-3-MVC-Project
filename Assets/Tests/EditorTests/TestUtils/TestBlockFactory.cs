using System.Collections.Generic;
using UnityEngine;
using Model.Objects;
using Model.Services;

namespace TestUtils
{
    public static class TestBlockFactory
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
        public static Block CreateBlockInCell(int typeId, Cell cell, GameBoard gameBoard = null)
        {
            var type = CreateBlockType(typeId);
            var block = new Block(type, cell.Position);
            cell.Block = block;
            cell.Block.Position = cell.Position;

            if (gameBoard != null)
            {
                gameBoard.Blocks.Add(block);
            }

            return block;
        }

        public static BasicBlockType CreateBlockType(int typeId) => new BasicBlockType(typeId);
    }
}