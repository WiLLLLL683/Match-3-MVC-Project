using Model.Objects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Model.Systems
{
    /// <summary>
    /// Система смены соседних блоков местами
    /// </summary>
    public class MoveSystem : IMoveSystem
    {
        private GameBoard gameBoard;
        //private MatchSystem matchSystem; 

        public MoveSystem(GameBoard _gameBoard) //, MatchSystem _matchSystem)
        {
            gameBoard = _gameBoard;
            //matchSystem = _matchSystem;
        }

        public SwapBlocksAction Move(Vector2Int _startPosition, Directions direction)
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
                    return null;
            }

            //проверка: начальная позиция вне поля?
            if (!Helpers.CheckValidCellByPosition(gameBoard, _startPosition))
                return null;

            //проверка: конечная позиция вне поля?
            if (!Helpers.CheckValidCellByPosition(gameBoard, targetPosition))
                return null;

            //возврат действия по смене блоков местами
            return new SwapBlocksAction(gameBoard.cells[_startPosition.x, _startPosition.y], gameBoard.cells[targetPosition.x, targetPosition.y]);
        }
    }
}