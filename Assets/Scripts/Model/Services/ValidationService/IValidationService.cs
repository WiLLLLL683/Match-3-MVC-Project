using Model.Objects;
using System.Collections.Generic;
using UnityEngine;

namespace Model.Services
{
    public interface IValidationService
    {
        /// <summary>
        /// Блок существует в заданной позиции
        /// </summary>
        public bool BlockExistsAt(Vector2Int position);

        /// <summary>
        /// Клетка существует в заданной позиции
        /// </summary>
        public bool CellExistsAt(Vector2Int position);

        /// <summary>
        /// Клетка пуста и может содержать в себе блок
        /// </summary>
        public bool CellIsEmptyAt(Vector2Int position);

        /// <summary>
        /// Найти все пустые клетки, которые могу содержать блок
        /// </summary>
        HashSet<Cell> FindEmptyCells();
    }
}