using Model.Services;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

namespace Model.Objects
{
    public interface IBooster
    {
        int Id { get; }

        /// <summary>
        /// Использовать бустер. Возвращает клетки для уничтожения блоков в них.
        /// </summary>
        HashSet<Cell> Execute(Vector2Int startPosition, GameBoard gameboard, IValidationService validationService);

        /// <summary>
        /// Memberwise clone.
        /// </summary>
        IBooster Clone();
    }
}