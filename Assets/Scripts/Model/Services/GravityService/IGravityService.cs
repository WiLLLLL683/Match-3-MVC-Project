using Model.Objects;

namespace Model.Services
{
    /// <summary>
    /// Сервис с правилами работы гравитации
    /// </summary>
    public interface IGravityService
    {
        /// <summary>
        /// Переместить все "висящие в воздухе" блоки вниз
        /// </summary>
        void Execute(GameBoard gameBoard);
    }
}