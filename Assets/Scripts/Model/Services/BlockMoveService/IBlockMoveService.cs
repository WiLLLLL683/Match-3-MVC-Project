using System;
using UnityEngine;
using Model.Objects;
using Utils;
using System.Threading.Tasks;

namespace Model.Services
{
    /// <summary>
    /// Сервис перемещения блоков
    /// </summary>
    public interface IBlockMoveService
    {
        event Action<Block> OnPositionChange;
        event Action<Block, Vector2Int> OnFlyStarted;

        /// <summary>
        /// Сдвинуть блок в необходимую позицию со сменой блоков местами
        /// возвращает успех перемещения
        /// </summary>
        bool Move(Vector2Int startPosition, Vector2Int targetPosition);

        /// <summary>
        /// Сдвинуть блок в необходимую сторону со сменой блоков местами
        /// возвращает успех перемещения
        /// </summary>
        bool Move(Vector2Int startPosition, Directions direction);

        /// <summary>
        /// Запустить блок в полет до целевой позиции с ожиданием задержки.
        /// </summary>
        Task FlyAsync(Vector2Int startPosition, Vector2Int targetPosition);

        /// <summary>
        /// Перемешать блоки в игровой зоне.
        /// </summary>
        void ShuffleAllBlocks();
    }
}