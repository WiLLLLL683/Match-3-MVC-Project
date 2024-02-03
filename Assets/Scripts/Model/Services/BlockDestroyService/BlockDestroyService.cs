using Model.Objects;
using System;
using System.Collections.Generic;
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

        public BlockDestroyService(Game game, IValidationService validation, ICellSetBlockService setBlockService)
        {
            this.game = game;
            this.validation = validation;
            this.setBlockService = setBlockService;
        }

        public void MarkToDestroy(Vector2Int position)
        {
            if (!validation.BlockExistsAt(position))
                return;

            GameBoard.Cells[position.x, position.y].Block.isMarkedToDestroy = true;
        }

        public List<Block> FindMarkedBlocks()
        {
            List<Block> markedBlocks = new();

            for (int x = 0; x < GameBoard.Cells.GetLength(0); x++)
            {
                for (int y = 0; y < GameBoard.HiddenRowsStartIndex; y++)
                {
                    Block block = validation.TryGetBlock(new(x, y));

                    if (block != null)
                        markedBlocks.Add(block);
                }
            }

            return markedBlocks;
        }

        public List<ICounterTarget> DestroyAllMarkedBlocks()
        {
            List<ICounterTarget> destroyedTargets = new();
            List<Block> markedBlocks = FindMarkedBlocks();

            for (int i = 0; i < markedBlocks.Count; i++)
            {
                ICounterTarget counterTarget = markedBlocks[i].Type;

                if (TryDestroy(markedBlocks[i].Position))
                {
                    destroyedTargets.Add(counterTarget);
                }
            }

            return destroyedTargets;
        }

        private bool TryDestroy(Vector2Int position)
        {
            if (!validation.BlockExistsAt(position))
                return false;

            Cell cell = GameBoard.Cells[position.x, position.y];

            if (!cell.Block.isMarkedToDestroy)
                return false;

            OnDestroy?.Invoke(cell.Block);
            GameBoard.Blocks.Remove(cell.Block);
            setBlockService.SetEmpty(cell);
            return true;
        }
    }
}