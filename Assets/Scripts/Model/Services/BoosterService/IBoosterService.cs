using Model.Objects;

namespace Model.Services
{
    /// <summary>
    /// Сервис для работы с бустерами
    /// </summary>
    public interface IBoosterService
    {
        /// <summary>
        /// Добавить бустер определенного типа
        /// </summary>
        void AddBooster<T>(int ammount) where T : IBooster;

        /// <summary>
        /// Забрать бустер определенного типа
        /// </summary>
        IBooster SpendBooster<T>() where T : IBooster, new();

        /// <summary>
        /// Получить количество бустеров определенного типа
        /// </summary>
        public int GetBoosterAmount<T>() where T : IBooster;
    }
}