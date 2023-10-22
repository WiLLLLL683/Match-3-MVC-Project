using Model.Objects;
using System;

namespace Model.Services
{
    /// <summary>
    /// Сервис для обновления счетчиков
    /// </summary>
    public interface ICounterService
    {
        bool CheckCompletion(Counter counter);

        event Action<Counter> OnCompleteEvent;
        event Action<Counter> OnUpdateEvent;

        /// <summary>
        /// Проверка на совпадение с целью счетчика, уменьшение счета при совпадении
        /// </summary>
        public void CheckTarget(Counter counter, ICounterTarget target);
    }
}