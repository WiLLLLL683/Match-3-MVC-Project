using Model.Objects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Model.Systems
{
    /// <summary>
    /// ������� ����� �������� ������ �������
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
            //��������� �������� �������
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

            //��������: ��������� ������� ��� ����?
            if (!Helpers.CheckValidCellByPosition(gameBoard, _startPosition))
                return null;

            //��������: �������� ������� ��� ����?
            if (!Helpers.CheckValidCellByPosition(gameBoard, targetPosition))
                return null;

            //������� �������� �� ����� ������ �������
            return new SwapBlocksAction(gameBoard.cells[_startPosition.x, _startPosition.y], gameBoard.cells[targetPosition.x, targetPosition.y]);
        }
    }
}