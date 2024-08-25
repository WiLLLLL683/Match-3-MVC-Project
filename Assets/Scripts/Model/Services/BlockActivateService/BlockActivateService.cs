using Config;
using Cysharp.Threading.Tasks;
using Model.Objects;
using System;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Model.Services
{
    public class BlockActivateService : IBlockActivateService
    {
        private readonly Game game;
        private readonly IValidationService validationService;

        private GameBoard GameBoard => game.CurrentLevel.gameBoard;

        public BlockActivateService(Game game, IValidationService validationService)
        {
            this.game = game;
            this.validationService = validationService;
        }

        public List<Block> FindMarkedBlocks()
        {
            List<Block> markedBlocks = new();

            foreach (Block block in GameBoard.Blocks)
            {
                if (block?.isMarkedToDestroy == false)
                    continue;

                markedBlocks.Add(block);
            }

            return markedBlocks;
        }

        public async UniTask ActivateMarkedBlocks()
        {
            foreach (Block block in FindMarkedBlocks())
            {
                await block.Type.Activate(block.Position, Directions.Zero);
            }
        }

        public async UniTask ActivateBlock(Vector2Int position, Directions direction)
        {
            Block block = validationService.TryGetBlock(position);
            if (block == null)
                return;

            await block.Type.Activate(position, direction);
        }
    }
}