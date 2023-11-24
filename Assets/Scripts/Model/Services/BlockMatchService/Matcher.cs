using Model.Objects;
using System.Collections.Generic;
using UnityEngine;

namespace Model.Services
{
    public class Matcher : IMatcher
    {
        private readonly HashSet<Cell> matchedCells = new();
        private readonly IValidationService validation;

        private GameBoard gameBoard;
        private Pattern pattern;
        private Vector2Int startPosition;
        private Vector2Int originPos;
        private int originTypeId;

        public Matcher(IValidationService validation)
        {
            this.validation = validation;
        }

        public HashSet<Cell> MatchAt(Vector2Int startPosition, Pattern pattern, GameBoard gameBoard)
        {
            this.startPosition = startPosition;
            this.pattern = pattern;
            this.gameBoard = gameBoard;

            matchedCells.Clear();

            if (pattern.totalSum == 0)
                return matchedCells;

            CalculateOriginPosition();

            if (!validation.BlockExistsAt(originPos))
                return matchedCells;

            FindSimilarBlocks(pattern, gameBoard);
            CheckResult();
            return matchedCells;
        }

        private void CalculateOriginPosition() => originPos = pattern.originPosition + startPosition;

        private void FindSimilarBlocks(Pattern pattern, GameBoard gameBoard)
        {
            originTypeId = gameBoard.Cells[originPos.x, originPos.y].Block.Type.Id;

            for (int x = 0; x < pattern.grid.GetLength(0); x++)
            {
                for (int y = 0; y < pattern.grid.GetLength(1); y++)
                {
                    CompareBlockWithOrigin(x, y);
                }
            }
        }

        private void CheckResult()
        {
            //для успеха должны совпасть все блоки помеченные в паттерне, иначе возвращается пустой набор клеток
            if (matchedCells.Count != pattern.totalSum)
            {
                matchedCells.Clear();
            }
        }

        private void CompareBlockWithOrigin(int x, int y)
        {
            if (!pattern.grid[x, y]) //помечена ли клетка в паттерне?
                return;

            Vector2Int posOnGameboard = new(x + startPosition.x, y + startPosition.y);
            if (!validation.BlockExistsAt(posOnGameboard))
                return;

            Cell cell = gameBoard.Cells[posOnGameboard.x, posOnGameboard.y];
            int cerrentTypeId = cell.Block.Type.Id;

            if (cerrentTypeId == originTypeId)
            {
                matchedCells.Add(cell);
            }
        }
    }
}