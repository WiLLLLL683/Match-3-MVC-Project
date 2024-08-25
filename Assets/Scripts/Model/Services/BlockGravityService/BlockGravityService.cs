using Config;
using Cysharp.Threading.Tasks;
using Infrastructure.Commands;
using Model.Objects;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace Model.Services
{
    public class BlockGravityService : IBlockGravityService
    {
        private readonly Game game;
        private readonly IValidationService validationService;
        private readonly IBlockMoveService moveService;
        private readonly IConfigProvider configProvider;

        private GameBoard GameBoard => game.CurrentLevel.gameBoard;

        public BlockGravityService(Game game,
            IValidationService validationService,
            IBlockMoveService moveService,
            IConfigProvider configProvider)
        {
            this.game = game;
            this.validationService = validationService;
            this.moveService = moveService;
            this.configProvider = configProvider;
        }

        public async UniTask Execute(List<Cell> emptyCells, CancellationToken token = default)
        {
            for (int y = 0; y < GameBoard.Cells.GetLength(1); y++)
            {
                for (int i = 0; i < emptyCells.Count; i++)
                {
                    TryMoveBlockDown(emptyCells[i].Position.x, y, token);
                }
                await UniTask.WaitForSeconds(configProvider.Delays.betweenBlockGravitation, cancellationToken: token);
            }
        }

        public async UniTask Execute(CancellationToken token = default)
        {
            for (int y = 0; y < GameBoard.Cells.GetLength(1); y++)
            {
                for (int x = 0; x < GameBoard.Cells.GetLength(0); x++)
                {
                    TryMoveBlockDown(x, y, token);
                }
                await UniTask.WaitForSeconds(configProvider.Delays.betweenBlockGravitation, cancellationToken: token);
            }
        }

        private void TryMoveBlockDown(int x, int y, CancellationToken token)
        {
            if (!validationService.BlockExistsAt(new Vector2Int(x, y)))
                return;

            int lowestY = FindLowestEmptyCellUnderPos(x, y);
            if (y == lowestY)
                return;

            moveService.Move(new Vector2Int(x, y), new Vector2Int(x, lowestY));
        }

        private int FindLowestEmptyCellUnderPos(int x, int y)
        {
            for (int i = 0; i < y; i++)
            {
                if (validationService.CellIsEmptyAt(new(x, i)))
                {
                    return i;
                }
            }

            return y;
        }
    }
}