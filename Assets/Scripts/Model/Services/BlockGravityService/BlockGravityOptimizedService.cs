using Cysharp.Threading.Tasks;
using Infrastructure.Commands;
using Model.Objects;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using Utils;

namespace Model.Services
{
    public class BlockGravityOptimizedService : IBlockGravityService
    {
        private readonly Game game;
        private readonly IValidationService validationService;
        private readonly IBlockMoveService moveService;

        private GameBoard GameBoard => game.CurrentLevel.gameBoard;

        public BlockGravityOptimizedService(Game game,
            IValidationService validationService,
            IBlockMoveService moveService)
        {
            this.game = game;
            this.validationService = validationService;
            this.moveService = moveService;
        }

        public async UniTask Execute(CancellationToken token = default)
        {
            List<Cell> emptyCells = validationService.FindEmptyCells();
            await Execute(emptyCells, token);
            await UniTask.Delay(1000);
            Debug.Log("lol");
        }

        public async UniTask Execute(List<Cell> emptyCells, CancellationToken token = default)
        {
            for (int i = 0; i < emptyCells.Count; i++)
            {
                await ShiftBlocksColumnDown(emptyCells[i], token);
            }
        }

        private async UniTask ShiftBlocksColumnDown(Cell emptyCell, CancellationToken token = default)
        {
            int x = emptyCell.Position.x;

            for (int y = emptyCell.Position.y + 1; y < GameBoard.Cells.GetLength(1); y++) //снизу вверх
            {
                moveService.Move(new(x,y), Directions.Down);
                await UniTask.Yield(token);
            }
        }
    }
}