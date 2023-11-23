using Model.Objects;
using System.Collections.Generic;

namespace Model.Services
{
    public interface IBlockMatchService
    {
        /// <summary>
        /// Найти все совпадения по всем паттернам
        /// </summary>
        public HashSet<Cell> FindAllMatches();

        /// <summary>
        /// Найти первый попавшийся паттерн для подсказки
        /// </summary>
        public HashSet<Cell> FindHint();
    }
}