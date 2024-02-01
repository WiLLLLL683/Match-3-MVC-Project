using Model.Objects;
using System;

namespace Model.Services
{
    public interface IWinLoseService
    {
        void RaiseLoseEvent();
        void RaiseWinEvent();

        event Action OnLose;
        event Action OnWin;

        /// <summary>
        /// Уменьшить счетчик заданной цели, если он существует в целях или ограничениях текущего уровня
        /// </summary>
        void TryDecreaseCount(ICounterTarget target, int amount = 1);

        /// <summary>
        /// Увеличить счетчик заданной цели, если он существует в целях или ограничениях текущего уровня
        /// </summary>
        void TryIncreaseCount(ICounterTarget target, int amount = 1);

        /// <summary>
        /// Проверить закончились ли огранияения уровня
        /// </summary>
        public bool CheckLose();

        /// <summary>
        /// Проверить все ли цели уровня выполнены
        /// </summary>
        public bool CheckWin();
    }
}