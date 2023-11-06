using Model.Objects;
using System.Collections.Generic;

namespace Model.Services
{
    public class MatchService : IMatchService
    {
        private readonly Game game;
        private readonly IMatcher matcher;
        private readonly HashSet<Cell> matchedCells = new();

        private GameBoard GameBoard => game.CurrentLevel.gameBoard;
        private MatchPattern[] MatchPatterns => game.CurrentLevel.matchPatterns;
        private bool PatternFitGameboard => yLength >= 0 && xLength >= 0;

        private int xStartPos;
        private int yStartPos;
        private int xLength;
        private int yLength;

        public MatchService(Game game, IValidationService validationService)
        {
            this.game = game;
            matcher = new Matcher(validationService); //TODO вынести создание в Game
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
                    CheckPatternFirst(MatchPatterns[i].hintPatterns[j]);

                    if (matchedCells.Count > 0)
                        break;
                }
            }

            return matchedCells; //TODO вернуть только клетки которые нужно сменить для подсказки
        }

        /// <summary>
        /// Пройти по всем клеткам игрового поля(кроме невидимых) и сохранить все совпавшие клетки
        /// </summary>
        private void CheckPattern(Pattern pattern)
        {
            SetRegionToCheck(pattern);

            if (!PatternFitGameboard)
                return;

            for (int y = yStartPos; y <= yLength; y++)
            {
                for (int x = xStartPos; x <= xLength; x++)
                {
                    HashSet<Cell> cells = matcher.MatchAt(new(x, y), pattern, GameBoard);
                    matchedCells.UnionWith(cells);
                }
            }
        }

        /// <summary>
        /// Пройти по всем клеткам игрового поля(кроме невидимых) и вернуть клетки первого совпадения паттерна
        /// </summary>
        private void CheckPatternFirst(Pattern pattern)
        {
            SetRegionToCheck(pattern);

            if (!PatternFitGameboard)
                return;

            for (int y = yStartPos; y <= yLength; y++)
            {
                for (int x = xStartPos; x <= xLength; x++)
                {
                    HashSet<Cell> cells = matcher.MatchAt(new(x, y), pattern, GameBoard);
                    matchedCells.UnionWith(cells);

                    if (matchedCells.Count > 0)
                        break;
                }
            }

            //TODO вернуть только клетки которые нужно сменить для подсказки
        }

        private void SetRegionToCheck(Pattern pattern)
        {
            xStartPos = 0;
            xLength = GameBoard.Cells.GetLength(0) - pattern.grid.GetLength(0);

            yStartPos = GameBoard.RowsOfInvisibleCells;
            yLength = GameBoard.Cells.GetLength(1) - pattern.grid.GetLength(1) - GameBoard.RowsOfInvisibleCells;
        }
    }
}