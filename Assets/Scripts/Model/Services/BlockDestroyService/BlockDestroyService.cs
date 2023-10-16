using UnityEngine;
using Model.Factories;
using Model.Objects;
using System;

namespace Model.Services
{
    public class BlockDestroyService : IBlockDestroyService
    {
        private readonly IValidationService validation;
        private readonly IBlockFactory blockFactory;
        private GameBoard gameBoard;

        public event Action<Block> OnDestroy;

        public BlockDestroyService(IValidationService validationService, IBlockFactory blockFactory)
        {
            this.validation = validationService;
            this.blockFactory = blockFactory;
        }

        public void SetLevel(GameBoard gameBoard) => this.gameBoard = gameBoard;

        public IAction Destroy(Vector2Int position)
        {
            if (!validation.CellExistsAt(position))
                return null;

            Cell cell = gameBoard.Cells[position.x, position.y];

            return Destroy(cell);
        }

        public IAction Destroy(Cell cell)
        {
            if (!validation.BlockExistsAt(cell.Position))
                return null;

            DestroyBlockAction action = new DestroyBlockAction(gameBoard, cell, blockFactory);
            action.Execute();
            OnDestroy?.Invoke(action.Block);
            return action;
        }
    }
}