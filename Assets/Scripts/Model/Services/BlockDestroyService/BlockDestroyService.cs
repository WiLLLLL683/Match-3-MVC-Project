using UnityEngine;
using Model.Factories;
using Model.Objects;
using System;

namespace Model.Services
{
    public class BlockDestroyService : IBlockDestroyService
    {
        private readonly IValidationService validation;
        private readonly ICellSetBlockService setBlockService;
        private GameBoard gameBoard;

        public event Action<Block> OnDestroy;

        public BlockDestroyService(IValidationService validationService, ICellSetBlockService setBlockService)
        {
            this.validation = validationService;
            this.setBlockService = setBlockService;
        }

        public void SetLevel(GameBoard gameBoard) => this.gameBoard = gameBoard;

        public void DestroyAt(Vector2Int position)
        {
            if (!validation.CellExistsAt(position))
                return;

            Cell cell = gameBoard.Cells[position.x, position.y];
            DestroyAt(cell);
        }

        public void DestroyAt(Cell cell)
        {
            if (!validation.BlockExistsAt(cell.Position))
                return;

            gameBoard.Blocks.Remove(cell.Block);
            setBlockService.SetEmpty(cell);
            OnDestroy?.Invoke(cell.Block);
        }
    }
}