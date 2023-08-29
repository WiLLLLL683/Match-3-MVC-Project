using Data;
using Model.Objects;
using System.Collections.Generic;
using UnityEngine;

namespace Model.Services
{
    public interface IMatcher
    {
        /// <summary>
        /// Наложить паттерн на игровое поле в заданной позиции
        /// </summary>
        HashSet<Cell> MatchAt(Vector2Int startPosition, Pattern pattern, GameBoard gameBoard);
    }
}