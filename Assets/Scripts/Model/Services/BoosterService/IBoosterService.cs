using Model.Objects;

namespace Model.Readonly
{
    /// <summary>
    /// Сервис для работы с бустерами
    /// </summary>
    public interface IBoosterService : IBoosterService_Readonly
    {
        /// <summary>
        /// Добавить бустер определенного типа
        /// </summary>
        void AddBooster<T>(int ammount) where T : IBooster;

        /// <summary>
        /// Забрать бустер определенного типа
        /// </summary>
        IBooster SpendBooster<T>() where T : IBooster, new();
    }
}