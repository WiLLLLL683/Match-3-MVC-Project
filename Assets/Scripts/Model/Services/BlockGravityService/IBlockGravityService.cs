using Cysharp.Threading.Tasks;
using Model.Objects;
using System.Collections.Generic;
using System.Threading;

namespace Model.Services
{
    /// <summary>
    /// Сервис с правилами работы гравитации
    /// </summary>
    public interface IBlockGravityService
    {
        /// <summary>
        /// Переместить вниз столбцы блоков над заданными пустыми клетками
        /// </summary>
        UniTask Execute(List<Cell> emptyCells, CancellationToken token = default);

        /// <summary>
        /// Переместить вниз все "висящие в воздухе" блоки
        /// </summary>
        UniTask Execute(CancellationToken token = default);
    }
}