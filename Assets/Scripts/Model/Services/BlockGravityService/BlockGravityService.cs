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

        private GameBoard GameBoard => game.CurrentLevel.gameBoard;

        private int lowestY;

        public BlockGravityService(Game game, IValidationService validationService, IBlockMoveService moveService)
        {
            this.game = game;
            this.validationService = validationService;
            this.moveService = moveService;
        }

        public async UniTask Execute(List<Cell> emptyCells, CancellationToken token = default)
        {
            await Execute(token);
        }

        public async UniTask Execute(CancellationToken token = default)
        {
            for (int y = 0; y < GameBoard.Cells.GetLength(1); y++) //проверка снизу вверх чтобы не было ошибок
            {
                for (int x = 0; x < GameBoard.Cells.GetLength(0); x++)
                {
                    TryMoveBlockDown(x, y);
                }
            }
        }

        private void TryMoveBlockDown(int x, int y)
        {
            if (!validationService.BlockExistsAt(new Vector2Int(x, y)))
                return;

            int lowestY = FindLowestEmptyCellUnderPos(x, y);
            if (y == lowestY)
                return;

            var action = new BlockMoveCommand(new(x, y), new(x, lowestY), moveService); //TODO возвращать комманду?
            action.Execute();
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