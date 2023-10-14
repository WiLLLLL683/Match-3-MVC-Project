using Model.Objects;
using System.Collections.Generic;

namespace Model.Services
{
    public class MatchService : IMatchService
    {
        private readonly HashSet<Cell> matchedCells = new();
        private readonly IMatcher matcher;

        private GameBoard gameBoard;
        private MatchPattern[] matchPatterns;

        private int xStartPos;
        private int yStartPos;
        private int xLength;
        private int yLength;

        private bool PatternFitGameboard => yLength >= 0 && xLength >= 0;

        public MatchService(IValidationService validationService)
        {
            matcher = new Matcher(validationService); //TODO вынести создание в Game
        }

        public void SetLevel(GameBoard gameBoard, MatchPattern[] matchPatterns)
        {
            this.gameBoard = gameBoard;
            this.matchPatterns = matchPatterns;
        }

        public HashSet<Cell> FindAllMatches()
        {
            matchedCells.Clear();

            for (int i = 0; i < matchPatterns.Length; i++)
            {
                CheckPattern(matchPatterns[i]);
            }

            return matchedCells;
        }

        public HashSet<Cell> FindHint()
        {
            matchedCells.Clear();

            for (int i = 0; i < matchPatterns.Length; i++)
            {
                for (int j = 0; j < matchPatterns[i].hintPatterns.Length; j++)
                {
                    CheckPatternFirst(matchPatterns[i].hintPatterns[j]);

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
                    HashSet<Cell> cells = matcher.MatchAt(new(x, y), pattern, gameBoard);
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
                    HashSet<Cell> cells = matcher.MatchAt(new(x, y), pattern, gameBoard);
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
            xLength = gameBoard.Cells.GetLength(0) - pattern.grid.GetLength(0);

            yStartPos = gameBoard.RowsOfInvisibleCells;
            yLength = gameBoard.Cells.GetLength(1) - pattern.grid.GetLength(1) - gameBoard.RowsOfInvisibleCells;
        }
    }
}