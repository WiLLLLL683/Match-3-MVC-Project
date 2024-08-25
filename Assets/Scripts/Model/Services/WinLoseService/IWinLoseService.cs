using Model.Objects;
using System;

namespace Model.Services
{
    public interface IWinLoseService
    {
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
        /// Проверить содержит ли текущий уровень ограничение заданного типа
        /// </summary>
        bool TryGetRestriction(ICounterTarget target, out Counter counter);

        /// <summary>
        /// Проверить содержит ли текущий уровень цель заданного типа
        /// </summary>
        bool TryGetGoal(ICounterTarget target, out Counter counter);

        /// <summary>
        /// Проверить закончились ли все огранияения текущего уровня
        /// </summary>
        public bool CheckLose();

        /// <summary>
        /// Проверить выполнены ли все цели текущего уровня выполнены
        /// </summary>
        public bool CheckWin();

        /// <summary>
        /// Запуск события проигрыша
        /// </summary>
        void RaiseLoseEvent();

        /// <summary>
        /// Запуск события победы
        /// </summary>
        void RaiseWinEvent();
    }
}