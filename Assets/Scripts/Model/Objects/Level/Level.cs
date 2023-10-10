using Model.Readonly;
using System;

namespace Model.Objects
{
    /// <summary>
    /// Объект уровня с игровой доской и правилами
    /// </summary>
    [Serializable]
    public class Level : ILevel_Readonly
    {
        public GameBoard gameBoard;
        public Counter[] goals;
        public Counter[] restrictions;
        public Pattern[] matchPatterns;
        public HintPattern[] hintPatterns;

        public event Action OnWin;
        public event Action OnLose;

        public IGameBoard_Readonly GameBoard_Readonly => gameBoard;
        public ICounter_Readonly[] Goals_Readonly => goals;
        public ICounter_Readonly[] Restrictions_Readonly => goals;

        /// <summary>
        /// Проверить все ли цели уровня выполнены
        /// </summary>
        public bool CheckWin()
        {
            for (int i = 0; i < goals.Length; i++)
            {
                if (goals[i] == null)
                    continue;

                if (!goals[i].IsCompleted)
                    return false;
            }

            OnWin?.Invoke();
            return true;
        }

        /// <summary>
        /// Проверить закончились ли огранияения уровня
        /// </summary>
        public bool CheckLose()
        {
            for (int i = 0; i < restrictions.Length; i++)
            {
                if (restrictions[i] == null)
                    continue;

                if (restrictions[i].IsCompleted)
                {
                    OnLose?.Invoke();
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Пересчет счетчика целей уровня, с вычетом 1 цели
        /// </summary>
        public void UpdateGoals(ICounterTarget _target)
        {
            for (int i = 0; i < goals.Length; i++)
            {
                goals[i].CheckTarget(_target);
            }
        }

        /// <summary>
        /// Пересчет счетчика ограничений уровня, с вычетом 1 цели
        /// </summary>
        public void UpdateRestrictions(ICounterTarget _target)
        {
            for (int i = 0; i < restrictions.Length; i++)
            {
                restrictions[i].CheckTarget(_target);
            }
        }
    }
}