using System;
using Model.Objects;
using UnityEngine;

namespace Model.Services
{
    public class BlockChangeTypeService : IBlockChangeTypeService
    {
        private readonly IValidationService validation;
        private GameBoard gameBoard;

        public event Action<Block> OnTypeChange;

        public BlockChangeTypeService(IValidationService validationService)
        {
            this.validation = validationService;
        }

        public void SetLevel(GameBoard gameBoard) => this.gameBoard = gameBoard;

        public void ChangeBlockType(Vector2Int position, BlockType targetType)
        {
            if (!validation.CellExistsAt(position))
                return;

            Cell cell = gameBoard.Cells[position.x, position.y];
            ChangeBlockType(cell, targetType);
        }

        public void ChangeBlockType(Cell cell, BlockType targetType)
        {
            if (!validation.BlockExistsAt(cell.Position))
                return;

            if (targetType == null)
                return;

            cell.Block.Type = targetType;
            OnTypeChange?.Invoke(cell.Block);
        }
    }
}