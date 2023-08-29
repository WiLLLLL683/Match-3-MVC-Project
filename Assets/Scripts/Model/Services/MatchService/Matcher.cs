using Data;
using Model.Objects;
using System;
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

        private bool PatternIsEmpty => pattern.totalSum == 0;
        private bool AllCellsMatched => matchedCells.Count == pattern.totalSum;
        private Vector2Int OriginPos => new(pattern.originPosition.x + startPosition.x, pattern.originPosition.y + startPosition.y);
        private int OriginTypeId => gameBoard.cells[OriginPos.x, OriginPos.y].Block.Type.Id;

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

            if (PatternIsEmpty)
                return matchedCells;

            if (!validation.BlockExistsAt(OriginPos))
                return matchedCells;

            for (int x = 0; x < pattern.grid.GetLength(0); x++)
            {
                for (int y = 0; y < pattern.grid.GetLength(1); y++)
                {
                    CompareBlockWithOrigin(x, y);
                }
            }

            if (!AllCellsMatched)
                matchedCells.Clear();

            return matchedCells;
        }

        private void CompareBlockWithOrigin(int x, int y)
        {
            if (!pattern.grid[x, y]) //помечена ли клетка в паттерне?
                return;

            Vector2Int posOnGameboard = new(x + startPosition.x, y + startPosition.y);
            if (!validation.BlockExistsAt(posOnGameboard))
                return;

            Cell cell = gameBoard.cells[posOnGameboard.x, posOnGameboard.y];
            int cerrentTypeId = cell.Block.Type.Id;

            if (cerrentTypeId == OriginTypeId)
            {
                matchedCells.Add(cell);
            }
        }
    }
}