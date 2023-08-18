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
        public Cell[,] cells;
        public int rowsOfInvisibleCells;
        public List<Block> blocks = new List<Block>();

        public event Action<IBlock_Readonly> OnBlockSpawn;

        public ICell_Readonly[,] Cells_Readonly => cells;
        public IEnumerable<IBlock_Readonly> Blocks_Readonly => blocks;
        public int RowsOfInvisibleCells => rowsOfInvisibleCells;

        public GameBoard() { }

        /// <summary>
        /// For tests only
        /// Создание пустого игрового поля с базовыми клетками по заданным размерам
        /// </summary>
        public GameBoard(int xLength, int yLength)
        {
            cells = new Cell[xLength, yLength];
            blocks = new List<Block>();

            for (int y = 0; y < yLength; y++)
            {
                for (int x = 0; x < xLength; x++)
                {
                    cells[x, y] = new Cell(new BasicCellType(), new Vector2Int(x, y));
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
                blocks.Add(block);
                block.OnDestroy += UnRegisterBlock;
                OnBlockSpawn?.Invoke(block);
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

            //может ли клетка иметь блок?
            if (!cells[position.x, position.y].CanContainBlock)
            {
                Debug.LogWarning("Tried to get Block but Cell was notPlayable");
                return false;
            }

            //есть ли блок в клетке?
            if (cells[position.x, position.y].IsEmpty)
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
                position.x < cells.GetLength(0) &&
                position.y < cells.GetLength(1))
            {
                return true;
            }
            else
            {
                Debug.LogWarning("Cell position out of GameBoards range");
                return false;
            }
        }
        private void UnRegisterBlock(Block block) => blocks.Remove(block);
    }
}
