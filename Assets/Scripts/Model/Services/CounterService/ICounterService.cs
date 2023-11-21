using Model.Objects;
using System;

namespace Model.Services
{
    /// <summary>
    /// Сервис для обновления счетчиков
    /// </summary>
    public interface ICounterService
    {
        event Action<Counter> OnCompleteEvent;
        event Action<Counter> OnUpdateEvent;

        /// <summary>
        /// Увеличить счет если цель счетчика совпадает
        /// </summary>
        void IncreaseCount(Counter counter, ICounterTarget target, int amount);

        /// <summary>
        /// Уменьшить счет если цель счетчика совпадает
        /// </summary>
        void DecreaseCount(Counter counter, ICounterTarget target, int amount);

        /// <summary>
        /// Проверить завершен ли отсчет
        /// </summary>
        bool CheckCompletion(Counter counter);
    }
}