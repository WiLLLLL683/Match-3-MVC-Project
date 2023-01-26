using Model;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model.Objects
{
     /// <summary>
    /// Паттерн для нахождения одинаковых блоков, выстроенных в ряд
    /// </summary>
    public class Pattern
    {
        private bool[,] grid;
        private int totalSum; //сумма помеченых клеток в паттерне
        private Vector2Int originPosition = new Vector2Int(0, 0);
        private Type originType;

        public Pattern(bool[,] _grid)
        {
            grid = _grid;
            for (int x = 0; x < grid.GetLength(0); x++)
            {
                for (int y = 0; y < grid.GetLength(1); y++)
                {
                    if (grid[x, y] == true)
                    {
                        //взять положение любой помеченной клетки чтобы позже найти оригинал для сравнения
                        originPosition.x = x;
                        originPosition.y = y;
                        //подсчитать сумму помеченных клеток
                        totalSum++;
                    }
                }
            }
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
    }
}