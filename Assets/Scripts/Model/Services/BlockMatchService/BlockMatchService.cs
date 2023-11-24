using Model.Objects;
using System.Collections.Generic;
using UnityEngine;

namespace Model.Services
{
    /// <summary>
    /// Поиск совпадений происходит путем "наложения" сетки паттерна на сетку игрового поля.
    /// Паттерн проверяется в Matcher, затем сдвигается дальше по всему региону сравнения.
    /// За опорную точку принята позиция [0,0] в координатах паттерна.
    /// </summary>
    public class BlockMatchService : IBlockMatchService
    {
        private readonly Game game;
        private readonly IMatcher matcher;
        private readonly HashSet<Cell> matchedCells = new();

        private GameBoard GameBoard => game.CurrentLevel.gameBoard;
        private MatchPattern[] MatchPatterns => game.CurrentLevel.matchPatterns;
        private bool IsPatternFitGameboard => checkBounds.height > 0 && checkBounds.width > 0;

        private RectInt checkBounds;

        public BlockMatchService(Game game, IMatcher matcher)
        {
            this.game = game;
            this.matcher = matcher;
        }

        public HashSet<Cell> FindAllMatches()
        {
            matchedCells.Clear();

            for (int i = 0; i < MatchPatterns.Length; i++)
            {
                CheckPattern(MatchPatterns[i]);
            }

            return matchedCells;
        }

        public HashSet<Cell> FindHint()
        {
            matchedCells.Clear();

            for (int i = 0; i < MatchPatterns.Length; i++)
            {
                for (int j = 0; j < MatchPatterns[i].hintPatterns.Length; j++)
                {
                    bool isHintFound = CheckPatternFirst(MatchPatterns[i].hintPatterns[j]);

                    if (isHintFound)
                        return matchedCells;
                }
            }

            return matchedCells; //TODO вернуть только клетки которые нужно сменить для подсказки
        }

        /// <summary>
        /// Пройти по всем клеткам игрового поля(кроме невидимых) и сохранить все совпавшие клетки
        /// </summary>
        private void CheckPattern(Pattern pattern)
        {
            CalculateCheckBounds(pattern);

            if (!IsPatternFitGameboard)
                return;

            for (int y = checkBounds.yMin; y <= checkBounds.yMax; y++)
            {
                for (int x = checkBounds.xMin; x <= checkBounds.xMax; x++)
                {
                    HashSet<Cell> cells = matcher.MatchAt(new(x, y), pattern, GameBoard);
                    matchedCells.UnionWith(cells);
                }
            }
        }

        /// <summary>
        /// Пройти по всем клеткам игрового поля(кроме невидимых) и сохранить клетки первого совпадения паттерна
        /// Возвращает true при успешном поиске
        /// </summary>
        private bool CheckPatternFirst(Pattern pattern)
        {
            CalculateCheckBounds(pattern);

            if (!IsPatternFitGameboard)
                return false;

            for (int y = checkBounds.yMin; y <= checkBounds.yMax; y++)
            {
                for (int x = checkBounds.xMin; x <= checkBounds.xMax; x++)
                {
                    HashSet<Cell> cells = matcher.MatchAt(new(x, y), pattern, GameBoard);
                    matchedCells.UnionWith(cells);

                    if (matchedCells.Count > 0)
                        return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Ограничить регион сравнения по размерам паттерна, чтобы не искать совпадения за пределами игрового поля
        /// или в рядах невидимых клеток, предназначенных для спавна блоков
        /// </summary>
        private void CalculateCheckBounds(Pattern pattern)
        {
            checkBounds.xMin = 0;
            checkBounds.yMin = GameBoard.RowsOfInvisibleCells;

            checkBounds.xMax = GameBoard.Cells.GetLength(0) - pattern.grid.GetLength(0) + 1; //+1 для учета опорной точки
            checkBounds.yMax = GameBoard.Cells.GetLength(1) - pattern.grid.GetLength(1) + 1;
        }
    }
}