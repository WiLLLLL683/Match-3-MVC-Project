using Model.Objects;
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
        private Level level;

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
            if (!level.gameBoard.ValidateBlockAt(targetPosition) ||
                !level.gameBoard.ValidateBlockAt(startPosition))
            {
                return null;
            }

            //возврат действия по смене блоков местами
            return new SwapBlocksAction(level.gameBoard.Cells[startPosition.x, startPosition.y], level.gameBoard.Cells[targetPosition.x, targetPosition.y]);
        }
    }
}