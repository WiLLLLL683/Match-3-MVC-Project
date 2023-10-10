using Model.Readonly;
using System;

namespace Model.Objects
{
    /// <summary>
    /// ������ ������ � ������� ������ � ���������
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
        /// ��������� ��� �� ���� ������ ���������
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
        /// ��������� ����������� �� ����������� ������
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
        /// �������� �������� ����� ������, � ������� 1 ����
        /// </summary>
        public void UpdateGoals(ICounterTarget _target)
        {
            for (int i = 0; i < goals.Length; i++)
            {
                goals[i].CheckTarget(_target);
            }
        }

        /// <summary>
        /// �������� �������� ����������� ������, � ������� 1 ����
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