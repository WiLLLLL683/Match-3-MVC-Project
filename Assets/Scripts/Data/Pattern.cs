using Array2DEditor;
using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;
using Model.Objects;
using System;
using Model.Services;

namespace Data
{
     /// <summary>
    /// ѕаттерн дл€ нахождени€ одинаковых блоков, выстроенных в р€д
    /// </summary>
    [CreateAssetMenu(fileName ="Pattern", menuName ="Data/Pattern")]
    public class Pattern: ScriptableObject, ICloneable
    {
        //дл€ Unity
        [SerializeField] protected Array2DBool array2d;

        //статичные данные
        public bool[,] grid;
        public int totalSum; //сумма помеченых клеток в паттерне
        public Vector2Int originPosition = new Vector2Int(0, 0);

        public Pattern(bool[,] _grid)
        {
            grid = _grid;
            originPosition = GetOriginPosition();
            totalSum = CalculateTotalSum();
        }

        private void OnValidate()
        {
            grid = GetGridFromArray2d(array2d);
            originPosition = GetOriginPosition();
            totalSum = CalculateTotalSum();
        }

        /// <summary>
        /// Ќайти клетки совпадающие с данным паттерном
        /// </summary>
        public HashSet<Cell> Match(GameBoard gameBoard, Vector2Int startPosition, IValidationService validationService)
        {
            //пуст ли паттерн?
            if (totalSum == 0)
                return new HashSet<Cell>();

            //есть ли блок в начальной позиции?
            Vector2Int originPosOnGameboard = new(originPosition.x + startPosition.x, originPosition.y + startPosition.y);
            if (!validationService.BlockExistsAt(originPosOnGameboard))
                return new HashSet<Cell>();

            //вз€ть тип оригинального блока
            int originTypeId = gameBoard.cells[originPosOnGameboard.x, originPosOnGameboard.y].Block.Type.Id;

            //подсчитать совпадени€, пройд€ по всем координатам паттерна
            int sum = 0;
            HashSet<Cell> matchedCells = new();

            for (int x = 0; x < grid.GetLength(0); x++)
            {
                for (int y = 0; y < grid.GetLength(1); y++)
                {
                    Vector2Int posOnGameboard = new(x + startPosition.x, y + startPosition.y);

                    //помечена ли клетка в паттерне?
                    if (!grid[x, y])
                        continue;

                    //есть ли блок в клетке?
                    if (!validationService.BlockExistsAt(posOnGameboard))
                        continue;

                    Cell cell = gameBoard.cells[posOnGameboard.x, posOnGameboard.y];
                    Block block = cell.Block;

                    //совпадают ли типы блоков?
                    if (block.Type.Id == originTypeId)
                    {
                        sum++;
                        matchedCells.Add(cell);
                    }
                }
            }

            //все ли помеченные клетки паттерна совпали?
            if (sum == totalSum)
                return matchedCells;
            else
                return new HashSet<Cell>();
        }

        public object Clone() => this.MemberwiseClone();



        protected Vector2Int GetOriginPosition()
        {
            Vector2Int pos = new();

            for (int x = 0; x < grid.GetLength(0); x++)
            {
                for (int y = 0; y < grid.GetLength(1); y++)
                {
                    if (grid[x, y] == true)
                    {
                        pos.x = x;
                        pos.y = y;
                        return pos;
                    }
                }
            }

            return pos;
        }
        protected int CalculateTotalSum()
        {
            int sum = 0;

            for (int x = 0; x < grid.GetLength(0); x++)
            {
                for (int y = 0; y < grid.GetLength(1); y++)
                {
                    if (grid[x, y] == true)
                    {
                        sum++;
                    }
                }
            }

            return sum;
        }
        protected bool[,] GetGridFromArray2d(Array2DBool _data)
        {
            bool[,] grid = new bool[_data.GridSize.x, _data.GridSize.y];

            for (int i = 0; i < _data.GridSize.x; i++)
            {
                for (int j = 0; j < _data.GridSize.y; j++)
                {
                    grid[i,j] = _data.GetCell(i,j);
                }
            }

            return grid;
        }
    }
}