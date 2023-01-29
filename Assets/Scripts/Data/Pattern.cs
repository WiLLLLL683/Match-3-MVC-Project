using Array2DEditor;
using Model.Objects;
using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
     /// <summary>
    /// Паттерн для нахождения одинаковых блоков, выстроенных в ряд
    /// </summary>
    [CreateAssetMenu(fileName ="Pattern", menuName ="Data/Pattern")]
    public class Pattern: ScriptableObject
    {
        //для Unity
        [SerializeField] protected Array2DBool array2d;

        //статичные данные
        protected bool[,] grid;
        [ShowNonSerializedField] protected int totalSum; //сумма помеченых клеток в паттерне
        [ShowNonSerializedField] protected Vector2Int originPosition = new Vector2Int(0, 0);

        //переменные
        protected Type originType;

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

        public List<Cell> Match(GameBoard _gameBoard, Vector2Int _startPosition)
        {
            //пуст ли паттерн?
            if (totalSum == 0)
                return new List<Cell>();

            Vector2Int originPosOnGameboard = new Vector2Int(originPosition.x + _startPosition.x, originPosition.y + _startPosition.y);

            //есть ли блок?
            if (!_gameBoard.CheckValidBlockByPosition(originPosOnGameboard))
                return new List<Cell>();

            //взять тип оригинального блока
            originType = _gameBoard.cells[originPosOnGameboard.x, originPosOnGameboard.y].block.type.GetType();

            int sum = 0;
            List<Cell> matchedCells = new List<Cell>();

            //проверить и подсчитать совпадения, пройдя по координатам паттерна
            for (int x = 0; x < grid.GetLength(0); x++)
            {
                for (int y = 0; y < grid.GetLength(1); y++)
                {
                    Vector2Int posOnGameboard = new Vector2Int(x + _startPosition.x, y + _startPosition.y);

                    //помечена ли клетка?
                    if (grid[x, y] == false)
                        continue;

                    //есть ли блок?
                    if (!_gameBoard.CheckValidBlockByPosition(posOnGameboard))
                        continue;

                    //совпадают ли типы блоков?
                    if (_gameBoard.cells[posOnGameboard.x, posOnGameboard.y].block.type.GetType().Equals(originType))
                    {
                        sum++;
                        matchedCells.Add(_gameBoard.cells[posOnGameboard.x, posOnGameboard.y]);
                    }
                }
            }

            //все ли помеченные клетки паттерна совпали?
            if (sum == totalSum)
                return matchedCells;
            else
                return new List<Cell>();
        }



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