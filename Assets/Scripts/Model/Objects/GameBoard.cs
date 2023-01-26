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
        public Cell[,] cells { get; private set; }
        public List<Block> blocks { get; private set; }

        [SerializeReference, SubclassSelector] private ACellType[,] cellTypes;

        /// <summary>
        /// Создание пустого игрового поля исходя из данных
        /// </summary>
        public GameBoard(ACellType[,] _cellTypes)
        {
            cellTypes = _cellTypes;

            cells = new Cell[cellTypes.GetLength(0), cellTypes.GetLength(1)];
            blocks = new List<Block>();

            for (int i = 0; i < cellTypes.GetLength(0); i++)
            {
                for (int j = 0; j < cellTypes.GetLength(1); j++)
                {
                    cells[i, j] = new Cell(cellTypes[i,j], new Vector2Int(i, j));
                }
            }
        }

        /// <summary>
        /// Создание пустого игрового поля с базовыми клетками по заданным размерам
        /// </summary>
        public GameBoard(int xLength, int yLength)
        {
            cells = new Cell[xLength, yLength];
            blocks = new List<Block>();

            for (int i = 0; i < xLength; i++)
            {
                for (int j = 0; j < yLength; j++)
                {
                    cells[i, j] = new Cell(new BasicCellType(), new Vector2Int(i,j));
                }
            }
        }

        public void RegisterBlock(Block _block)
        {
            if (_block != null)
            {
                blocks.Add(_block);
                _block.OnDestroy += UnRegisterBlock;
            }
        }
        public bool CheckValidBlockByPosition(Vector2Int _position)
        {
            //позиция вне границ игрового поля?
            if (!CheckValidCellByPosition(_position))
            {
                return false;
            }

            //играбельна ли клетка?
            if (!cells[_position.x, _position.y].IsPlayable)
            {
                Debug.LogError("Tried to get Block but Cell was notPlayable");
                return false;
            }

            //есть ли блок в клетке?
            if (cells[_position.x, _position.y].isEmpty)
            {
                Debug.LogError("Tried to get Block but Cell was empty");
                return false;
            }

            return true;
        }
        public bool CheckValidCellByPosition(Vector2Int _position)
        {
            //позиция в границах игрового поля?
            if (_position.x >= 0 &&
                _position.y >= 0 &&
                _position.x < cells.GetLength(0) &&
                _position.y < cells.GetLength(1))
            {
                return true;
            }
            else
            {
                Debug.LogError("Cell position out of GameBoards range");
                return false;
            }
        }


        private void UnRegisterBlock(Block _block, EventArgs eventArgs)
        {
            blocks.Remove(_block);
        }
    }
}
