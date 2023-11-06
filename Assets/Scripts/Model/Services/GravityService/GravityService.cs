using Model.Infrastructure.Commands;
using Model.Objects;
using UnityEngine;

namespace Model.Services
{
    public class GravityService : IGravityService
    {
        private readonly Game game;
        private readonly IValidationService validationService;
        private readonly IBlockMoveService moveService;

        private GameBoard GameBoard => game.CurrentLevel.gameBoard;

        private int lowestY;

        public GravityService(Game game, IValidationService validationService, IBlockMoveService moveService)
        {
            this.game = game;
            this.validationService = validationService;
            this.moveService = moveService;
        }

        public void Execute()
        {
            for (int y = GameBoard.Cells.GetLength(1); y >= 0; y--) //проверка снизу вверх чтобы не было ошибок
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

            FindLowestEmptyCellUnderPos(x, y);

            if (!IsLowestEmptyCell(y))
            {
                var action = new BlockMoveCommand(new(x, y), new(x, lowestY), moveService); //TODO возвращать комманду?
                action.Execute();
            }
        }

        private bool IsLowestEmptyCell(int y) => y == lowestY;

        private void FindLowestEmptyCellUnderPos(int x, int y)
        {
            lowestY = y;
            for (int i = GameBoard.Cells.GetLength(1) - 1; i > y; i--)
            {
                if (validationService.CellIsEmptyAt(new(x, i)))
                {
                    lowestY = i;
                    return;
                }
            }
        }
    }
}