using Model.Objects;
using Model.Services;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Model.Systems
{
    /// <summary>
    /// Система перемещения блоков
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
        /// Обновить данные об уровне
        /// </summary>
        public void SetLevel(Level level)
        {
            this.level = level;
        }

        /// <summary>
        /// Сдвинуть блок в необходимую сторону со сменой блоков местами
        /// </summary>
        public SwapBlocksAction Move(Vector2Int startPosition, Directions direction)
        {
            //вычислить конечную позицию
            Vector2Int targetPosition = startPosition + direction.ToVector2Int();

            //проверка: есть ли в начальной и конечной позициях блоки?
            if (!validationService.BlockExistsAt(targetPosition) ||
                !validationService.BlockExistsAt(startPosition))
            {
                return null;
            }

            //возврат действия по смене блоков местами
            return new SwapBlocksAction(level.gameBoard.cells[startPosition.x, startPosition.y], level.gameBoard.cells[targetPosition.x, targetPosition.y]);
        }
    }
}