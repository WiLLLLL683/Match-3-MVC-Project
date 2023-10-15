using Model.Objects;

namespace Model.Readonly
{
    /// <summary>
    /// Сервис для работы с бустерами
    /// </summary>
    public interface IBoosterService_Readonly
    {
        /// <summary>
        /// Получить количество бустеров определенного типа
        /// </summary>
        public int GetBoosterAmount<T>() where T : IBooster;
    }
}