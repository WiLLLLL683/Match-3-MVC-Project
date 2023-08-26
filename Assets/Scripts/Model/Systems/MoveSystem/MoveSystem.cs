using Model.Objects;
using Model.Services;
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
        private IValidationService validationService;
        private Level level;

        public MoveSystem(IValidationService validationService)
        {
            this.validationService = validationService;
        }

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

            //��������: ���� �� � ��������� � �������� �������� �����?
            if (!validationService.BlockExistsAt(targetPosition) ||
                !validationService.BlockExistsAt(startPosition))
            {
                return null;
            }

            //������� �������� �� ����� ������ �������
            return new SwapBlocksAction(level.gameBoard.cells[startPosition.x, startPosition.y], level.gameBoard.cells[targetPosition.x, targetPosition.y]);
        }
    }
}