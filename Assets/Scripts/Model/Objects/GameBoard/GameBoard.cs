using Data;
using Model.Readonly;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Model.Objects
{
    /// <summary>
    /// Объект игрового поля, хранит сетку клеток и все блоки на поле
    /// </summary>
    public class GameBoard : IGameBoard_Readonly
    {
        public Cell[,] Cells { get; private set; }
        public List<Block> Blocks { get; private set; }
        public ICell_Readonly[,] Cells_Readonly => Cells;
        public IEnumerable<IBlock_Readonly> Blocks_Readonly => Blocks;

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
                    Cells[i, j] = new Cell(cellTypes[i, j], new Vector2Int(i, j));
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
                    Cells[i, j] = new Cell(new BasicCellType(), new Vector2Int(i, j));
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
        public bool ValidateBlockAt(Vector2Int position)
        {
            //позиция вне границ игрового поля?
            if (!ValidateCellAt(position))
                return false;

            //играбельна ли клетка?
            if (!Cells[position.x, position.y].IsPlayable)
            {
                Debug.LogWarning("Tried to get Block but Cell was notPlayable");
                return false;
            }

            //есть ли блок в клетке?
            if (Cells[position.x, position.y].IsEmpty)
            {
                Debug.LogWarning("Tried to get Block but Cell was empty");
                return false;
            }

            return true;
        }



        private bool ValidateCellAt(Vector2Int position)
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
                Debug.LogWarning("Cell position out of GameBoards range");
                return false;
            }
        }
        private void UnRegisterBlock(Block block) => Blocks.Remove(block);
    }
}
