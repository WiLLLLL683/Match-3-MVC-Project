using Model.Objects;
using System.Collections.Generic;
using UnityEngine;

namespace Model.Services
{
    /// <summary>
    /// Компонент BlockMatchService для сравнения одного паттерна в одной точке игрового поля
    /// </summary>
    public interface IMatcher
    {
        /// <summary>
        /// Наложить паттерн на игровое поле в заданной позиции
        /// </summary>
        HashSet<Cell> MatchAt(Vector2Int startPosition, Pattern pattern, GameBoard gameBoard);
    }
}