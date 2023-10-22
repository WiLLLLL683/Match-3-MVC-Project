using Model.Commands;
using Model.Objects;
using UnityEngine;

namespace Model.Services
{
    public class GravityService : IGravityService
    {
        private readonly IValidationService validationService;
        private readonly IBlockMoveService moveService;
        private GameBoard gameBoard;

        private int lowestY;

        public GravityService(IValidationService validationService, IBlockMoveService moveService)
        {
            this.validationService = validationService;
            this.moveService = moveService;
        }

        public void SetLevel(GameBoard gameBoard) => this.gameBoard = gameBoard;

        public void Execute()
        {
            for (int y = gameBoard.Cells.GetLength(1); y >= 0; y--) //проверка снизу вверх чтобы не было ошибок
            {
                for (int x = 0; x < gameBoard.Cells.GetLength(0); x++)
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
            for (int i = gameBoard.Cells.GetLength(1) - 1; i > y; i--)
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