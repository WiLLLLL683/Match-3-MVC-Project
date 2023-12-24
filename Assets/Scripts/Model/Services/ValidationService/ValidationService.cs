using Model.Objects;
using System.Collections.Generic;
using UnityEngine;

namespace Model.Services
{
    public class ValidationService : IValidationService
    {
        private readonly Game game;

        private GameBoard GameBoard => game.CurrentLevel.gameBoard;
        private bool BlockExists => GameBoard.Cells[position.x, position.y].Block != null;
        private bool CellIsEmpty => GameBoard.Cells[position.x, position.y].Block == null;
        private bool CellCanContainBlock => GameBoard.Cells[position.x, position.y].Type.CanContainBlock;
        private bool CellExists => GameBoard.Cells[position.x, position.y] != null;
        private bool CellIsInsideGameboard => 0 <= position.x && position.x < GameBoard.Cells.GetLength(0)
                                           && 0 <= position.y && position.y < GameBoard.Cells.GetLength(1);

        private Vector2Int position;

        public ValidationService(Game game)
        {
            this.game = game;
        }

        public bool BlockExistsAt(Vector2Int position)
        {
            this.position = position;

            if (!CellExistsAt(position))
            {
                return false;
            }

            if (!CellCanContainBlock)
            {
                Debug.LogWarning("Tried to get Block but Cell cant contain Block");
                return false;
            }

            if (CellIsEmpty)
            {
                Debug.LogWarning("Tried to get Block but Cell was empty");
                return false;
            }

            if (!BlockExists)
            {
                Debug.LogWarning("Tried to get Block but Block was null");
                return false;
            }

            return true;
        }

        public bool CellExistsAt(Vector2Int position)
        {
            this.position = position;

            if (!CellIsInsideGameboard)
            {
                Debug.LogWarning("Cell position out of GameBoards range");
                return false;
            }

            if (!CellExists)
            {
                Debug.LogWarning("Tried to get Cell but Cell was null");
                return false;
            }

            return true;
        }

        public bool CellIsEmptyAt(Vector2Int position)
        {
            this.position = position;

            if (!CellExistsAt(position))
            {
                return false;
            }

            if (!CellCanContainBlock)
            {
                Debug.LogWarning("Cell cant contain Block");
                return false;
            }

            if (!CellIsEmpty)
            {
                Debug.LogWarning("Cell is not empty");
                return false;
            }

            return true;
        }

        public HashSet<Cell> FindEmptyCells()
        {
            Cell[,] cells = game.CurrentLevel.gameBoard.Cells;
            int invisibleRows = game.CurrentLevel.gameBoard.RowsOfInvisibleCells;
            HashSet<Cell> emptyCells = new();

            for (int y = invisibleRows; y < cells.GetLength(1); y++)
            {
                for (int x = 0; x < cells.GetLength(0); x++)
                {
                    if (CellIsEmptyAt(new(x, y)))
                    {
                        emptyCells.Add(cells[x, y]);
                    }
                }
            }

            return emptyCells;
        }
    }
}