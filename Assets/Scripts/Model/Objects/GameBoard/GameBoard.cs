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
        public int RowsOfInvisibleCells { get; private set; }
        public ICell_Readonly[,] Cells_Readonly => Cells;
        public IEnumerable<IBlock_Readonly> Blocks_Readonly => Blocks;

        public event Action<IBlock_Readonly> OnBlockSpawn;

        private readonly ICellType invisibleCellType;

        /// <summary>
        /// Создание пустого игрового поля исходя из данных
        /// </summary>
        public GameBoard(ICellType[,] cellTypes, int rowsOfInvisibleCells, ICellType invisibleCellType)
        {
            this.invisibleCellType = invisibleCellType;
            this.RowsOfInvisibleCells = rowsOfInvisibleCells;

            int xLength = cellTypes.GetLength(0);
            int yLength = cellTypes.GetLength(1) + RowsOfInvisibleCells;
            Cells = new Cell[xLength, yLength];
            Blocks = new List<Block>();

            //спавн невидимых клеток
            for (int y = 0; y < RowsOfInvisibleCells; y++)
            {
                for (int x = 0; x < xLength; x++)
                {
                    Cells[x, y] = new Cell(this.invisibleCellType, new Vector2Int(x, y));
                }
            }

            //спавн обычных клеток
            for (int y = RowsOfInvisibleCells; y < yLength; y++)
            {
                for (int x = 0; x < xLength; x++)
                {
                    Cells[x, y] = new Cell(cellTypes[x, y - RowsOfInvisibleCells], new Vector2Int(x, y));
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

            for (int y = 0; y < yLength; y++)
            {
                for (int x = 0; x < xLength; x++)
                {
                    Cells[x, y] = new Cell(new BasicCellType(), new Vector2Int(x, y));
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
            if (!Cells[position.x, position.y].CanContainBlock)
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
