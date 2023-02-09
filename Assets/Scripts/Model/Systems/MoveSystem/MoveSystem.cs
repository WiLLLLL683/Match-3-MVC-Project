using Model.Objects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Model.Systems
{
    /// <summary>
    /// ������� ����������� ������
    /// </summary>
    public class MoveSystem : IMoveSystem
    {
        private Level level;

        /// <summary>
        /// �������� ������ �� ������
        /// </summary>
        public void SetLevel(Level _level)
        {
            level = _level;
        }

        /// <summary>
        /// �������� ���� � ����������� ������� �� ������ ������ �������
        /// </summary>
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
            if (!level.gameBoard.CheckValidCellByPosition(_startPosition))
                return null;

            //��������: �������� ������� ��� ����?
            if (!level.gameBoard.CheckValidCellByPosition(targetPosition))
                return null;

            //������� �������� �� ����� ������ �������
            return new SwapBlocksAction(level.gameBoard.Cells[_startPosition.x, _startPosition.y], level.gameBoard.Cells[targetPosition.x, targetPosition.y]);

            //TODO �������� ������ ��������� �������
        }



        /// <summary>
        /// for tests only
        /// </summary>
        public Level GetLevel() => level;
    }
}