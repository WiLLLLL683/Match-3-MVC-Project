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

        public GravitySystem(GameBoard _gameBoard)
        {
            gameBoard = _gameBoard;
        }

        public void Execute()
        {
            for (int x = 0; x < gameBoard.cells.GetLength(0); x++)
            {
                for (int y = gameBoard.cells.GetLength(1); y >= 0; y--) //проверка снизу вверх чтобы не было ошибок
                {
                    TryMoveBlockDown(x, y);
                }
            }
        }

        private void TryMoveBlockDown(int x, int y)
        {
            int lowestY = y;
            for (int i = gameBoard.cells.GetLength(1) - 1; i > y; i--)
            {
                if (gameBoard.cells[x, i].isEmpty)
                {
                    lowestY = i;
                    break;
                }
            }

            if (lowestY == y)
            {
                return;
            }
            else
            {
                SwapBlocksAction action = new SwapBlocksAction(gameBoard.cells[x, y], gameBoard.cells[x, lowestY]);
                action.Execute();
            }
        }
    }
}