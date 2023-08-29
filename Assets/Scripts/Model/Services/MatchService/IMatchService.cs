using Data;
using Model.Objects;
using System.Collections.Generic;

namespace Model.Services
{
    public interface IMatchService
    {
        /// <summary>
        /// Обновить данные об уровне
        /// </summary>
        public void SetLevel(GameBoard gameBoard);

        /// <summary>
        /// Пройти по всем клеткам игрового поля(кроме невидимых) и сохранить все совпавшие клетки
        /// </summary>
        public HashSet<Cell> Match(Pattern pattern);

        /// <summary>
        /// Пройти по всем клеткам игрового поля(кроме невидимых) и вернуть клетки первого совпавпадающего паттерна
        /// </summary>
        public HashSet<Cell> MatchFirst(Pattern pattern);
    }
}