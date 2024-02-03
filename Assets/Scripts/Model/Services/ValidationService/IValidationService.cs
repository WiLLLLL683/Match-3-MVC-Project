using Model.Objects;
using System.Collections.Generic;
using UnityEngine;

namespace Model.Services
{
    public interface IValidationService
    {
        /// <summary>
        /// Блок существует в заданной позиции.
        /// </summary>
        bool BlockExistsAt(Vector2Int position);

        /// <summary>
        /// Возвращает блок, если он существует в заданной позиции. Либо возвращает null.
        /// </summary>
        Block TryGetBlock(Vector2Int position);

        /// <summary>
        /// Клетка существует в заданной позиции.
        /// </summary>
        bool CellExistsAt(Vector2Int position);

        /// <summary>
        /// Клетка пуста и может содержать в себе блок.
        /// </summary>
        bool CellIsEmptyAt(Vector2Int position);

        /// <summary>
        /// Найти все пустые клетки, которые могут содержать блок.
        /// Порядок: колоннами слева-направо, внутри колонны сверху-вниз
        /// </summary>
        List<Cell> FindEmptyCells();
    }
}