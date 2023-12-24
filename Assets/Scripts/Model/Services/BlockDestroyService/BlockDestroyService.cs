using Model.Objects;
using System;
using UnityEngine;

namespace Model.Services
{
    public class BlockDestroyService : IBlockDestroyService
    {
        private readonly Game game;
        private readonly IValidationService validation;
        private readonly ICellSetBlockService setBlockService;

        public event Action<Block> OnDestroy;

        private GameBoard GameBoard => game.CurrentLevel.gameBoard;

        public BlockDestroyService(Game game, IValidationService validationService, ICellSetBlockService setBlockService)
        {
            this.game = game;
            this.validation = validationService;
            this.setBlockService = setBlockService;
        }

        public void DestroyAt(Vector2Int position)
        {
            if (!validation.CellExistsAt(position))
                return;

            Cell cell = GameBoard.Cells[position.x, position.y];
            DestroyAt(cell);
        }

        public void DestroyAt(Cell cell)
        {
            if (!validation.BlockExistsAt(cell.Position))
                return;

            OnDestroy?.Invoke(cell.Block);
            GameBoard.Blocks.Remove(cell.Block);
            setBlockService.SetEmpty(cell);
        }
    }
}