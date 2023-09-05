using Model.Objects;
using Model.Services;
using UnityEngine;

namespace Model.Systems
{
    /// <summary>
    /// Система для перемещения блоков вниз, если клетка внизу пуста
    /// </summary>
    public class GravitySystem : IGravitySystem
    {
        private IValidationService validationService;
        private GameBoard gameBoard;

        private int lowestY;

        public GravitySystem(IValidationService validationService)
        {
            this.validationService = validationService;
        }

        public void SetLevel(Level level) => gameBoard = level.gameBoard;
        public void SetGameBoard(GameBoard gameBoard) => this.gameBoard = gameBoard;

        /// <summary>
        /// Переместить все "висящие в воздухе" блоки вниз
        /// </summary>
        public void Execute(GameBoard gameBoard)
        {
            this.gameBoard = gameBoard;
            
            for (int y = gameBoard.cells.GetLength(1); y >= 0; y--) //проверка снизу вверх чтобы не было ошибок
            {
                for (int x = 0; x < gameBoard.cells.GetLength(0); x++)
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
                var action = new SwapBlocksAction(gameBoard.cells[x, y], gameBoard.cells[x, lowestY]);
                action.Execute();
            }
        }

        private bool IsLowestEmptyCell(int y) => y == lowestY;

        private void FindLowestEmptyCellUnderPos(int x, int y)
        {
            lowestY = y;
            for (int i = gameBoard.cells.GetLength(1) - 1; i > y; i--)
            {
                if (gameBoard.cells[x, i].IsEmpty && gameBoard.cells[x, i].CanContainBlock)
                {
                    lowestY = i;
                    return;
                }
            }
        }
    }
}