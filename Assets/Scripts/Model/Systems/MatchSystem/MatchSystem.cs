using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Model.Objects;
using Data;

namespace Model.Systems
{
    /// <summary>
    /// Система поиска одинаковых блоков выстроенных по паттернам
    /// </summary>
    public class MatchSystem : IMatchSystem
    {
        private Level level;

        /// <summary>
        /// Обновить данные об уровне
        /// </summary>
        public void SetLevel(Level _level)
        {
            level = _level;
        }

        /// <summary>
        /// Найти все совпадения по всем паттернам для уничтожения
        /// </summary>
        public HashSet<Cell> FindAllMatches()
        {
            HashSet<Cell> matchedCells = new();

            //Проверка всех паттернов
            for (int i = 0; i < level.matchPatterns.Length; i++)
            {
                HashSet<Cell> cellsAtPattern = CheckPattern(level.matchPatterns[i]);
                matchedCells.UnionWith(cellsAtPattern);
            }

            return matchedCells;
        }

        /// <summary>
        /// Найти первый попавшийся паттерн для подсказки
        /// </summary>
        public HashSet<Cell> FindFirstHint()
        {
            HashSet<Cell> matchedCells = new();

            for (int i = 0; i < level.hintPatterns.Length; i++)
            {
                HashSet<Cell> cellsAtPattern = CheckFirstPattern(level.hintPatterns[i]);
                matchedCells.UnionWith(cellsAtPattern);
            }

            return matchedCells; //TODO вернуть только клетки которые нужно сменить для подсказки
        }



        private HashSet<Cell> CheckPattern(Pattern _pattern)
        {
            HashSet<Cell> matchedCells = new();

            //пройти по всем клеткам игрового поля(кроме невидимых) и сохранить совпавшие клетки
            for (int x = 0; x < level.gameBoard.cells.GetLength(0); x++)
            {
                for (int y = 0; y < level.gameBoard.cells.GetLength(1) - level.gameBoard.RowsOfInvisibleCells; y++)
                {
                    HashSet<Cell> cellsAtPos = _pattern.Match(level.gameBoard, new Vector2Int(x, y));
                    matchedCells.UnionWith(cellsAtPos);
                }
            }

            //вернуть совпавшие клетки
            return matchedCells;
        }

        private HashSet<Cell> CheckFirstPattern(Pattern _pattern)
        {
            //пройти по всем клеткам игрового поля(кроме невидимых) и вернуть первые совпавшие клетки
            for (int x = 0; x < level.gameBoard.cells.GetLength(0); x++)
            {
                for (int y = 0; y < level.gameBoard.cells.GetLength(1) - level.gameBoard.RowsOfInvisibleCells; y++)
                {
                    HashSet<Cell> cellsAtPos = _pattern.Match(level.gameBoard, new Vector2Int(x, y));
                    if (cellsAtPos.Count > 0)
                    {
                        return cellsAtPos;
                    }
                }
            }

            //если совпадений нет, то вернуть пустой список
            return new HashSet<Cell>();
        }
    }
}