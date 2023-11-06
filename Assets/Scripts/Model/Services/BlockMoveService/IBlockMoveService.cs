using System;
using UnityEngine;
using Model.Objects;

namespace Model.Services
{
    /// <summary>
    /// Сервис перемещения блоков
    /// </summary>
    public interface IBlockMoveService
    {
        event Action<Block> OnPositionChange;

        /// <summary>
        /// Сдвинуть блок в необходимую позицию со сменой блоков местами
        /// возвращает успех перемещения
        /// </summary>
        public bool Move(Vector2Int startPosition, Vector2Int targetPosition);

        /// <summary>
        /// Сдвинуть блок в необходимую сторону со сменой блоков местами
        /// возвращает успех перемещения
        /// </summary>
        public bool Move(Vector2Int startPosition, Directions direction);
    }
}