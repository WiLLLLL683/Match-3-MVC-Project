using Model.Objects;
using System.Collections.Generic;

namespace Model.Services
{
    /// <summary>
    /// Сервис с правилами работы гравитации
    /// </summary>
    public interface IBlockGravityService
    {
        void Execute(List<Cell> emptyCells);

        /// <summary>
        /// Переместить все "висящие в воздухе" блоки вниз
        /// </summary>
        public void Execute();
    }
}