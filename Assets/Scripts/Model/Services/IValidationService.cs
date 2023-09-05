using Model.Objects;
using UnityEngine;

namespace Model.Services
{
    public interface IValidationService
    {
        public void SetLevel(GameBoard gameBoard);

        /// <summary>
        /// Проверка наличия блока в заданной позиции
        /// </summary>
        public bool BlockExistsAt(Vector2Int position);
        public bool CellExistsAt(Vector2Int position);
    }
}