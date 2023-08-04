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
        public void SetLevel(Level level)
        {
            this.level = level;
        }

        /// <summary>
        /// �������� ���� � ����������� ������� �� ������ ������ �������
        /// </summary>
        public SwapBlocksAction Move(Vector2Int startPosition, Directions direction)
        {
            //��������� �������� �������
            Vector2Int targetPosition = startPosition + direction.ToVector2Int();

            //��������: ��������� ������� ������ ����?
            if (!level.gameBoard.CheckValidCellByPosition(startPosition))
                return null;

            //��������: � ��������� ������� ���� ����?
            if (!level.gameBoard.CheckValidBlockByPosition(startPosition))
                return null;

            //��������: �������� ������� ������ ����?
            if (!level.gameBoard.CheckValidCellByPosition(targetPosition))
                return null;

            //��������: � �������� ������� ���� ����?
            if (!level.gameBoard.CheckValidBlockByPosition(startPosition))
                return null;

            //������� �������� �� ����� ������ �������
            return new SwapBlocksAction(level.gameBoard.Cells[startPosition.x, startPosition.y], level.gameBoard.Cells[targetPosition.x, targetPosition.y]);
        }
    }
}