using Cysharp.Threading.Tasks;
using Model.Objects;
using NUnit.Framework;
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

        public void MarkToDestroy(List<Block> blocks)
        {
            for (int i = 0; i < blocks.Count; i++)
            {
                if (blocks[i] == null)
                    return;

                blocks[i].isMarkedToDestroy = true;
            }
        }

        public void MarkToDestroyHorizontalLine(int y)
        {
            bool isInsideGameboard = (y >= 0) && (y < GameBoard.HiddenRowsStartIndex);
            if (!isInsideGameboard)
                return;

            for (int x = 0; x < GameBoard.Cells.GetLength(0); x++)
            {
                MarkToDestroy(new Vector2Int(x, y));
            }
        }

        public void MarkToDestroyVerticalLine(int x)
        {
            bool isInsideGameboard = (x >= 0) && (x < GameBoard.Cells.GetLength(0));
            if (!isInsideGameboard)
                return;

            for (int y = 0; y < GameBoard.HiddenRowsStartIndex; y++)
            {
                MarkToDestroy(new Vector2Int(x, y));
            }
        }

        public void MarkToDestroyRect(Vector2Int minBound, Vector2Int maxBound)
        {
            for (int x = minBound.x; x <= maxBound.x; x++)
            {
                for (int y = minBound.y; y <= maxBound.y; y++)
                {
                    MarkToDestroy(new Vector2Int(x, y));
                }
            }
        }

        public List<Block> FindMarkedBlocks()
        {
            List<Block> markedBlocks = new();

            for (int i = 0; i < GameBoard.Blocks.Count; i++)
            {
                Block block = GameBoard.Blocks[i];

                if (block != null && block.isMarkedToDestroy)
                    markedBlocks.Add(block);
            }

            return markedBlocks;
        }

        public void DestroyAllMarkedBlocks()
        {
            List<Block> markedBlocks = FindMarkedBlocks();

            for (int i = 0; i < markedBlocks.Count; i++)
            {
                if (!markedBlocks[i].isMarkedToDestroy)
                    continue;

                Destroy(markedBlocks[i]);
            }
        }

        private void Destroy(Block block)
        {
            Cell cell = GameBoard.Cells[block.Position.x, block.Position.y];
            GameBoard.Blocks.Remove(block);
            setBlockService.SetEmpty(cell);
            OnDestroy?.Invoke(block);
        }
    }
}