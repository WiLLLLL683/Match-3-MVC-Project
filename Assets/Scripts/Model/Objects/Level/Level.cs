using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data;
using Model.Systems;
using Model.Readonly;
using System;

namespace Model.Objects
{
    /// <summary>
    /// ������ ������ � ������� ������ � ���������
    /// </summary>
    public class Level : ILevel_Readonly
    {
        public GameBoard gameBoard;
        public Counter[] goals;
        public Counter[] restrictions;
        public Balance balance;
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
                if (!goals[i].isCompleted)
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
                if (restrictions[i].isCompleted)
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
                goals[i].UpdateGoal(_target);
            }
        }

        /// <summary>
        /// �������� �������� ����������� ������, � ������� 1 ����
        /// </summary>
        public void UpdateRestrictions(ICounterTarget _target)
        {
            for (int i = 0; i < restrictions.Length; i++)
            {
                restrictions[i].UpdateGoal(_target);
            }
        }
    }
}