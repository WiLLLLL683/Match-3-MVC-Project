using System;
using UnityEngine;
using Model.Objects;

namespace Model.Services
{
    public class BlockMoveService : IBlockMoveService
    {
        private readonly IValidationService validation;
        private readonly ICellSetBlockService setBlockService;
        private GameBoard gameBoard;

        public event Action<Block> OnPositionChange;

        public BlockMoveService(IValidationService validationService, ICellSetBlockService setBlockService)
        {
            this.validation = validationService;
            this.setBlockService = setBlockService;
        }

        public void SetLevel(GameBoard gameBoard) => this.gameBoard = gameBoard;

        public bool Move(Vector2Int startPosition, Directions direction)
        {
            Vector2Int targetPosition = startPosition + direction.ToVector2Int();
            return Move(startPosition, targetPosition);
        }

        public bool Move(Vector2Int startPosition, Vector2Int targetPosition)
        {
            if (!validation.BlockExistsAt(startPosition))
                return false;

            if (!validation.CellExistsAt(targetPosition))
                return false;

            Cell startCell = gameBoard.Cells[startPosition.x, startPosition.y];
            Cell targetCell = gameBoard.Cells[targetPosition.x, targetPosition.y];

            SwapTwoBlocks(startCell, targetCell);
            return true;
        }

        private void SwapTwoBlocks(Cell cellA, Cell cellB)
        {
            if (cellA != null && cellB != null)
            {
                Block tempBlock = cellA.Block;
                setBlockService.SetBlock(cellA, cellB.Block);
                setBlockService.SetBlock(cellB, tempBlock);

                OnPositionChange?.Invoke(cellA.Block);
                OnPositionChange?.Invoke(cellB.Block);
            }
        }
    }
}