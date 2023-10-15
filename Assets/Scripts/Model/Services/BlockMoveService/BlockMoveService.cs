using UnityEngine;
using Model.Objects;

namespace Model.Services
{
    public class BlockMoveService : IBlockMoveService
    {
        private readonly IValidationService validation;
        private GameBoard gameBoard;

        public BlockMoveService(IValidationService validationService)
        {
            this.validation = validationService;
        }

        public void SetLevel(GameBoard gameBoard) => this.gameBoard = gameBoard;

        public IAction Move(Vector2Int startPosition, Directions direction)
        {
            Vector2Int targetPosition = startPosition + direction.ToVector2Int();
            return Move(startPosition, targetPosition);
        }

        public IAction Move(Vector2Int startPosition, Vector2Int targetPosition)
        {
            if (!validation.BlockExistsAt(startPosition))
                return null;

            if (!validation.CellExistsAt(targetPosition))
                return null;

            Cell startCell = gameBoard.Cells[startPosition.x, startPosition.y];
            Cell targetCell = gameBoard.Cells[targetPosition.x, targetPosition.y];

            IAction action = new SwapBlocksAction(startCell, targetCell);
            action.Execute();
            return action;
        }
    }
}