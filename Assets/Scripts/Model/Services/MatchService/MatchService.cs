using Data;
using Model.Objects;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Model.Services
{
    public class MatchService : IMatchService
    {
        private readonly HashSet<Cell> matchedCells = new();
        private readonly IMatcher matcher;

        private GameBoard gameBoard;
        private int xLength;
        private int yLength;

        private bool PatternFitGameboard => yLength >= 0 && xLength >= 0;

        public MatchService(IValidationService validationService)
        {
            matcher = new Matcher(validationService); //TODO вынести создание в Game
        }

        public void SetLevel(GameBoard gameBoard) => this.gameBoard = gameBoard;

        public HashSet<Cell> Match(Pattern pattern)
        {
            Setup(pattern);

            if (!PatternFitGameboard)
                return matchedCells;

            for (int y = gameBoard.RowsOfInvisibleCells; y <= yLength; y++)
            {
                for (int x = 0; x <= xLength; x++)
                {
                    matchedCells.UnionWith(matcher.MatchAt(new(x, y), pattern, gameBoard));
                }
            }

            return matchedCells;
        }

        public HashSet<Cell> MatchFirst(Pattern pattern)
        {
            Setup(pattern);

            if (!PatternFitGameboard)
                return matchedCells;

            for (int y = gameBoard.RowsOfInvisibleCells; y <= yLength; y++)
            {
                for (int x = 0; x <= xLength; x++)
                {
                    matchedCells.UnionWith(matcher.MatchAt(new(x, y), pattern, gameBoard));

                    if (matchedCells.Count > 0)
                        break;
                }
            }

            return matchedCells;
        }

        private void Setup(Pattern pattern)
        {
            matchedCells.Clear();
            xLength = gameBoard.cells.GetLength(0) - pattern.grid.GetLength(0); //корректировка под размер паттерна
            yLength = gameBoard.cells.GetLength(1) - pattern.grid.GetLength(1) - gameBoard.RowsOfInvisibleCells; //корректировка под размер паттерна
        }
    }
}