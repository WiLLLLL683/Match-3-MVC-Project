using Model.Objects;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Model.Services
{
    public interface IBlockDestroyService
    {
        event Action<Block> OnDestroy;

        /// <summary>
        /// Пометить блок для уничтожения.
        /// </summary>
        void MarkToDestroy(Vector2Int position);

        /// <summary>
        /// Найти все помещенные для уничтожения блоки в игровой зоне.
        /// </summary>
        List<Block> FindMarkedBlocks();

        /// <summary>
        /// Уничтожить все помеченные блоки в игровой зоне. Возвращает лист уничтоженных целей для счетчика.
        /// </summary>
        List<ICounterTarget> DestroyAllMarkedBlocks();
    }
}