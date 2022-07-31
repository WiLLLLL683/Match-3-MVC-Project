using Model.Objects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Model.Systems
{
    /// <summary>
    /// Система смены соседних блоков местами
    /// </summary>
    public class SwitchSystem
    {
        private GameBoard gameBoard;

        public SwitchSystem(GameBoard _gameBoard)
        {
            gameBoard = _gameBoard;
        }

        public bool Switch(Vector2Int _startPosition, Directions direction)
        {
            //вычислить конечную позицию
            Vector2Int targetPosition;
            switch (direction)
            {
                case Directions.Up:
                    targetPosition = _startPosition + Vector2Int.up;
                    break;
                case Directions.Down:
                    targetPosition = _startPosition + Vector2Int.down;
                    break;
                case Directions.Left:
                    targetPosition = _startPosition + Vector2Int.left;
                    break;
                case Directions.Right:
                    targetPosition = _startPosition + Vector2Int.right;
                    break;
                default:
                    return false;
            }

            //проверка: начальная позиция вне поля?
            if (Helpers.CheckValidCellByPosition(gameBoard, _startPosition))
                return false;

            //проверка: конечная позиция вне поля?
            if (Helpers.CheckValidCellByPosition(gameBoard, targetPosition))
                return false;

            //проверка на результативность хода
            //TODO MatchSystem

            //смена блоков местами
            new SwapBlocksAction(gameBoard.cells[_startPosition.x, _startPosition.y], gameBoard.cells[targetPosition.x, targetPosition.y]).Execute();
            return true;
        }
    }
}