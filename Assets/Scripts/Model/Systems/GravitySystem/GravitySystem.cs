using Model.Objects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model.Systems
{
    /// <summary>
    /// Система для перемещения блоков вниз, если клетка внизу пуста
    /// </summary>
    public class GravitySystem : IGravitySystem
    {
        private GameBoard gameBoard;

        private int lowestY;

        public void SetLevel(Level level) => gameBoard = level.gameBoard;
        public void SetGameBoard(GameBoard gameBoard) => this.gameBoard = gameBoard;

        /// <summary>
        /// Переместить все "висящие в воздухе" блоки вниз
        /// </summary>
        public void Execute(GameBoard gameBoard)
        {
            this.gameBoard = gameBoard;
            
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
            if (!gameBoard.ValidateBlockAt(new Vector2Int(x, y)))
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
                if (gameBoard.Cells[x, i].IsEmpty && gameBoard.Cells[x, i].IsPlayable)
                {
                    lowestY = i;
                    return;
                }
            }
        }
    }
}