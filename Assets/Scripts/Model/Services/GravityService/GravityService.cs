using Model.Objects;
using UnityEngine;

namespace Model.Services
{
    public class GravityService : IGravityService
    {
        private IValidationService validationService;
        private GameBoard gameBoard;

        private int lowestY;

        public GravityService(IValidationService validationService)
        {
            this.validationService = validationService;
        }

        public void SetLevel(GameBoard gameBoard) => this.gameBoard = gameBoard;
        //public void SetGameBoard(GameBoard gameBoard) => this.gameBoard = gameBoard;

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
                var action = new SwapBlocksAction(gameBoard.Cells[x, y], gameBoard.Cells[x, lowestY]);
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