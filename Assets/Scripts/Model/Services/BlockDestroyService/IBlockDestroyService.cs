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
        /// Уничтожить все помеченные блоки. Возвращает лист уничтоженных целей для счетчика.
        /// </summary>
        List<ICounterTarget> DestroyAllMarkedBlocks();
    }
}