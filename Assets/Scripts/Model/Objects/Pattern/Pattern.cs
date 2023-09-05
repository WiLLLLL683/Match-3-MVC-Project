using System;
using System.Collections.Generic;
using UnityEngine;

namespace Model.Objects
{
    /// <summary>
    /// Паттерн для нахождения одинаковых блоков, выстроенных в ряд
    /// </summary>
    public class Pattern
    {
        public bool[,] grid;
        public int totalSum; //сумма помеченых клеток в паттерне
        public Vector2Int originPosition = new(0, 0);

        public Pattern(bool[,] grid)
        {
            this.grid = grid;
            originPosition = GetOriginPosition();
            totalSum = CalculateTotalSum();
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
    }
}