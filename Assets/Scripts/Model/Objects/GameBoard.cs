using Data;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model.Objects
{
    /// <summary>
    /// Объект игрового поля, хранит сетку клеток и все блоки на поле
    /// </summary>
    public class GameBoard
    {
        public Cell[,] Cells { get; private set; }
        public List<Block> Blocks { get; private set; }

        /// <summary>
        /// Создание пустого игрового поля исходя из данных
        /// </summary>
        public GameBoard(ACellType[,] cellTypes)
        {
            Cells = new Cell[cellTypes.GetLength(0), cellTypes.GetLength(1)];
            Blocks = new List<Block>();

            for (int i = 0; i < cellTypes.GetLength(0); i++)
            {
                for (int j = 0; j < cellTypes.GetLength(1); j++)
                {
                    Cells[i, j] = new Cell(cellTypes[i,j], new Vector2Int(i, j));
                }
            }
        }

        /// <summary>
        /// Создание пустого игрового поля с базовыми клетками по заданным размерам
        /// </summary>
        public GameBoard(int xLength, int yLength)
        {
            Cells = new Cell[xLength, yLength];
            Blocks = new List<Block>();

            for (int i = 0; i < xLength; i++)
            {
                for (int j = 0; j < yLength; j++)
                {
                    Cells[i, j] = new Cell(new BasicCellType(), new Vector2Int(i,j));
                }
            }
        }

        /// <summary>
        /// Регистрация блока в игровом поле
        /// </summary>
        public void RegisterBlock(Block block)
        {
            if (block != null)
            {
                Blocks.Add(block);
                block.OnDestroy += UnRegisterBlock;
            }
        }

        /// <summary>
        /// Проверка наличия блока в заданной позиции
        /// </summary>
        public bool CheckValidBlockByPosition(Vector2Int position)
        {
            //позиция вне границ игрового поля?
            if (!CheckValidCellByPosition(position))
            {
                return false;
            }

            //играбельна ли клетка?
            if (!Cells[position.x, position.y].IsPlayable)
            {
                Debug.LogError("Tried to get Block but Cell was notPlayable");
                return false;
            }

            //есть ли блок в клетке?
            if (Cells[position.x, position.y].IsEmpty)
            {
                Debug.LogError("Tried to get Block but Cell was empty");
                return false;
            }

            return true;
        }

        /// <summary>
        /// Проверка наличия клетки в границах игрового поля
        /// </summary>
        public bool CheckValidCellByPosition(Vector2Int position)
        {
            //позиция в границах игрового поля?
            if (position.x >= 0 &&
                position.y >= 0 &&
                position.x < Cells.GetLength(0) &&
                position.y < Cells.GetLength(1))
            {
                return true;
            }
            else
            {
                Debug.LogError("Cell position out of GameBoards range");
                return false;
            }
        }



        private void UnRegisterBlock(Block block) => Blocks.Remove(block);
    }
}
