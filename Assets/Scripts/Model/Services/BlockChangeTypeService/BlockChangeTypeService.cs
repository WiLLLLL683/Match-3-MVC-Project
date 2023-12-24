using Model.Objects;
using System;
using UnityEngine;

namespace Model.Services
{
    public class BlockChangeTypeService : IBlockChangeTypeService
    {
        private readonly Game game;
        private readonly IValidationService validation;

        public event Action<Block> OnTypeChange;

        private GameBoard GameBoard => game.CurrentLevel.gameBoard;

        public BlockChangeTypeService(Game game, IValidationService validationService)
        {
            this.game = game;
            this.validation = validationService;
        }

        public void ChangeBlockType(Vector2Int position, BlockType targetType)
        {
            if (!validation.CellExistsAt(position))
                return;

            Cell cell = GameBoard.Cells[position.x, position.y];
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