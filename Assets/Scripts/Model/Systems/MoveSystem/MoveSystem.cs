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
        public void SetLevel(Level _level)
        {
            level = _level;
        }

        /// <summary>
        /// Сдвинуть блок в необходимую сторону со сменой блоков местами
        /// </summary>
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
            if (!level.gameBoard.CheckValidCellByPosition(_startPosition))
                return null;

            //проверка: конечная позиция вне поля?
            if (!level.gameBoard.CheckValidCellByPosition(targetPosition))
                return null;

            //возврат действия по смене блоков местами
            return new SwapBlocksAction(level.gameBoard.Cells[_startPosition.x, _startPosition.y], level.gameBoard.Cells[targetPosition.x, targetPosition.y]);

            //TODO добавить ивенты различных исходов
        }



        /// <summary>
        /// for tests only
        /// </summary>
        public Level GetLevel() => level;
    }
}