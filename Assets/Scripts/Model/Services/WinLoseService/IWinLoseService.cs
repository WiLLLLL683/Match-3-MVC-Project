using Model.Objects;
using System;

namespace Model.Services
{
    public interface IWinLoseService
    {
        event Action OnLose;
        event Action OnWin;

        /// <summary>
        /// Проверить закончились ли огранияения уровня
        /// </summary>
        public bool CheckLose();

        /// <summary>
        /// Проверить все ли цели уровня выполнены
        /// </summary>
        public bool CheckWin();

        /// <summary>
        /// Пересчет счетчика целей уровня, с вычетом 1 цели
        /// </summary>
        public void UpdateGoals(ICounterTarget _target);

        /// <summary>
        /// Пересчет счетчика ограничений уровня, с вычетом 1 цели
        /// </summary>
        public void UpdateRestrictions(ICounterTarget _target);
    }
}