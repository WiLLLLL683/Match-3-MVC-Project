﻿using UnityEngine;

namespace Model.Objects
{
    /// <summary>
    /// Паттерн для нахождения блоков одного типа, расположенных в по заданной схеме
    /// </summary>
    public abstract class Pattern
    {
        /// <summary>
        /// Схема расположения блоков одного типа.
        /// [0,0] - нижняя левая клетка.
        /// True - тип блока должен совпадать с оригинальным.
        /// </summary>
        public bool[,] grid;

        /// <summary>
        /// Количество true клеток в схеме.
        /// </summary>
        public int totalSum;

        /// <summary>
        /// Положение оригинального блока с которым будут сравниваться остальные блоки, помеченные в схеме.
        /// Берется максимально близкая к [0,0] клетка.
        /// </summary>
        public Vector2Int originPosition = new(0, 0);

        protected Pattern(bool[,] grid)
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