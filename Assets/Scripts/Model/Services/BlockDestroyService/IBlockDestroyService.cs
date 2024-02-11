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
        /// Пометить блок к уничтожению.
        /// </summary>
        void MarkToDestroy(Vector2Int position);

        /// <summary>
        /// Пометить несколько блоков к уничтожению.
        /// </summary>
        void MarkToDestroy(List<Block> blocks);

        /// <summary>
        /// Пометить к уничтожению вертикальную линию из блоков на всю высоту игрового поля (кроме скрытых рядов блоков).
        /// </summary>
        void MarkToDestroyVerticalLine(int x);

        /// <summary>
        /// Пометить к уничтожению горизонтальную линию из блоков на всю ширину игрового поля.
        /// </summary>
        void MarkToDestroyHorizontalLine(int y);

        /// <summary>
        /// Пометить к уничтожению прямоугольник из блоков.
        /// </summary>
        void MarkToDestroyRect(Vector2Int minBound, Vector2Int maxBound);

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