using Model.Objects;
using UnityEngine;

namespace Model.Services
{
    public interface IValidationService
    {
        public void SetLevel(GameBoard gameBoard);

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
    }
}