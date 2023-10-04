using Model.Factories;
using Model.Objects;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Model.Services
{
    public class BlockDestroyService : IBlockDestroyService
    {
        private readonly IValidationService validation;
        private readonly IBlockFactory blockFactory;

        public BlockDestroyService(IValidationService validationService, IBlockFactory blockFactory)
        {
            this.validation = validationService;
            this.blockFactory = blockFactory;
        }

        public IAction Destroy(GameBoard gameBoard, Vector2Int position)
        {
            if (!validation.CellExistsAt(position))
                return null;

            Cell cell = gameBoard.Cells[position.x, position.y];

            return Destroy(gameBoard, cell);
        }

        public IAction Destroy(GameBoard gameBoard, Cell cell)
        {
            if (!validation.BlockExistsAt(cell.Position))
                return null;

            IAction action = new DestroyBlockAction(gameBoard, cell, blockFactory);
            action.Execute();
            return action;
        }
    }
}