using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Model.Objects;

namespace Model.Systems
{
    /// <summary>
    /// Система поиска одинаковых блоков выстроенных по паттернам
    /// </summary>
    public class MatchSystem
    {
        private Level level;

        public MatchSystem(Level _level)
        {
            level = _level;
        }

        /// <summary>
        /// Найти все совпадения по всем паттернам для уничтожения
        /// </summary>
        public List<Cell> FindMatches()
        {
            List<Cell> matchedCells = new List<Cell>();

            for (int i = 0; i < level.matchPatterns.Length; i++)
            {
                List<Cell> cellsAtPattern = CheckPattern(level.matchPatterns[i]);
                matchedCells.AddRange(cellsAtPattern);
            }

            return matchedCells;
        }

        /// <summary>
        /// Найти первый попавшийся паттерн для подсказки 
        /// </summary>
        public List<Cell> FindFirstHint()
        {
            List<Cell> matchedCells = new List<Cell>();

            for (int i = 0; i < level.hintPatterns.Length; i++)
            {
                List<Cell> cellsAtPattern = CheckFirstPattern(level.hintPatterns[i]);
                matchedCells.AddRange(cellsAtPattern);
            }

            return matchedCells; //TODO вернуть только клетки которые нужно сменить для подсказки
        }

        private List<Cell> CheckPattern(Pattern _pattern)
        {
            List<Cell> matchedCells = new List<Cell>();

            //пройти по всем клеткам игрового поля и сохранить совпавшие клетки
            for (int x = 0; x < level.gameBoard.cells.GetLength(0); x++)
            {
                for (int y = 0; y < level.gameBoard.cells.GetLength(1); y++)
                {
                    List<Cell> cellsAtPos = _pattern.Match(level.gameBoard, new Vector2Int(x,y));
                    matchedCells.AddRange(cellsAtPos);
                }
            }

            //вернуть совпавшие клетки
            return matchedCells;
        }

        private List<Cell> CheckFirstPattern(Pattern _pattern)
        {
            //пройти по всем клеткам игрового поля и вернуть первые совпавшие клетки
            for (int x = 0; x < level.gameBoard.cells.GetLength(0); x++)
            {
                for (int y = 0; y < level.gameBoard.cells.GetLength(1); y++)
                {
                    List<Cell> cellsAtPos = _pattern.Match(level.gameBoard, new Vector2Int(x, y));
                    if (cellsAtPos.Count > 0)
                    {
                        return cellsAtPos;
                    }
                }
            }

            //если совпадений нет, то вернуть пустой список
            return new List<Cell>();
        }
    }
}