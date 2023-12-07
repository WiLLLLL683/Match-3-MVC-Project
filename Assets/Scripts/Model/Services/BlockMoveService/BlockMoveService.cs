using System;
using UnityEngine;
using Model.Objects;
using Model.Infrastructure;
using Utils;

namespace Model.Services
{
    public class BlockMoveService : IBlockMoveService
    {
        public event Action<Block> OnPositionChange;

        private readonly Game game;
        private readonly IValidationService validation;
        private readonly ICellSetBlockService setBlockService;

        private GameBoard GameBoard => game.CurrentLevel.gameBoard;

        public BlockMoveService(Game game, IValidationService validationService, ICellSetBlockService setBlockService)
        {
            this.game = game;
            this.validation = validationService;
            this.setBlockService = setBlockService;
        }

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

            Cell startCell = GameBoard.Cells[startPosition.x, startPosition.y];
            Cell targetCell = GameBoard.Cells[targetPosition.x, targetPosition.y];

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