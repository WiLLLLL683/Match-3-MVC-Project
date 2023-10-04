using UnityEngine;
using Model.Objects;

namespace Model.Services
{
    /// <summary>
    /// Сервис перемещения блоков
    /// </summary>
    public interface IBlockMoveService
    {
        /// <summary>
        /// Сдвинуть блок в необходимую позицию со сменой блоков местами
        /// </summary>
        public IAction Move(GameBoard gameBoard, Vector2Int startPosition, Vector2Int targetPosition);

        /// <summary>
        /// Сдвинуть блок в необходимую сторону со сменой блоков местами
        /// </summary>
        public IAction Move(GameBoard gameBoard, Vector2Int startPosition, Directions direction);
    }
}