using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Model.Objects;

namespace Model.Services
{
    public class MoveService : IMoveService
    {
        private readonly IValidationService validation;

        public MoveService(IValidationService validationService)
        {
            this.validation = validationService;
        }

        public IAction Move(GameBoard gameBoard, Vector2Int startPosition, Directions direction)
        {
            Vector2Int targetPosition = startPosition + direction.ToVector2Int();
            return Move(gameBoard, startPosition, targetPosition);
        }

        public IAction Move(GameBoard gameBoard, Vector2Int startPosition, Vector2Int targetPosition)
        {
            if (!validation.BlockExistsAt(startPosition))
                return null;

            if (!validation.CellExistsAt(targetPosition))
                return null;

            Cell startCell = gameBoard.cells[startPosition.x, startPosition.y];
            Cell targetCell = gameBoard.cells[targetPosition.x, targetPosition.y];

            return new SwapBlocksAction(startCell, targetCell);
        }
    }
}