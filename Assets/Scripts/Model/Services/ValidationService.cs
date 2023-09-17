using Model.Objects;
using UnityEngine;

namespace Model.Services
{
    public class ValidationService : IValidationService
    {
        private GameBoard gameBoard;
        private Vector2Int position;

        private bool BlockExists => gameBoard.cells[position.x, position.y].Block != null;
        private bool CellIsEmpty => gameBoard.cells[position.x, position.y].IsEmpty;
        private bool CellCanContainBlock => gameBoard.cells[position.x, position.y].CanContainBlock;
        private bool CellExists => gameBoard.cells[position.x, position.y] != null;
        private bool CellIsInsideGameboard => 0 <= position.x && position.x < gameBoard.cells.GetLength(0)
                                           && 0 <= position.y && position.y < gameBoard.cells.GetLength(1);

        public void SetLevel(GameBoard gameBoard) => this.gameBoard = gameBoard;

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
    }
}