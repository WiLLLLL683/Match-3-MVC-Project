using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data;
using Model.Systems;

namespace Model.Objects
{
    /// <summary>
    /// ������ ������ � ������� ������ � ���������
    /// </summary>
    public class Level
    {
        public GameBoard gameBoard { get; private set; }
        public Counter[] goals { get; private set; }
        public Counter[] restrictions { get; private set; }
        public Balance balance { get; private set; }
        public Pattern[] matchPatterns { get; private set; }
        public Pattern[] hintPatterns { get; private set; }

        /// <summary>
        /// �������� ������ ������ �� ������ � ������ ������� �����
        /// </summary>
        /// <param name="levelData"></param>
        public Level(LevelData levelData)
        {
            if (levelData.ValidCheck() != true)
            {
                return;
            }

            gameBoard = new GameBoard(levelData.gameBoard);

            goals = new Counter[levelData.goals.Length];
            for (int i = 0; i < goals.Length; i++)
            {
                goals[i] = new Counter(levelData.goals[i]);
            }

            restrictions = new Counter[levelData.restrictions.Length];
            for (int i = 0; i < goals.Length; i++)
            {
                restrictions[i] = new Counter(levelData.restrictions[i]);
            }

            balance = new Balance(levelData.balance);

            //TODO �������� �������� ���������
        }

        /// <summary>
        /// for tests only
        /// </summary>
        public Level(int xLength, int yLength)
        {
            gameBoard = new GameBoard(xLength, yLength);
            goals = new Counter[1];
            goals[0] = new Counter(new BasicBlockType(),2);
            restrictions = new Counter[1];
            restrictions[0] = new Counter(new Turn(),2);

            Dictionary<ABlockType, int> balanceDictionary = new Dictionary<ABlockType, int>();
            balanceDictionary.Add(new BasicBlockType(), 50);
            balanceDictionary.Add(new BlueBlockType(), 50);
            balance = new Balance(balanceDictionary);
        }

        /// <summary>
        /// for tests only
        /// </summary>
        public Level(int xLength, int yLength, Pattern[] _matchPatterns)
        {
            gameBoard = new GameBoard(xLength, yLength);
            matchPatterns = _matchPatterns;
        }

        public bool CheckWin()
        {
            for (int i = 0; i < goals.Length; i++)
            {
                if (!goals[i].isCompleted)
                    return false;
            }

            return true;
        }

        public bool CheckLose()
        {
            for (int i = 0; i < restrictions.Length; i++)
            {
                if (restrictions[i].isCompleted)
                    return true;
            }

            return false;
        }

        public void UpdateGoals(ICounterTarget _target)
        {
            for (int i = 0; i < goals.Length; i++)
            {
                goals[i].UpdateGoal(_target);
            }
        }

        public void UpdateRestrictions(ICounterTarget _target)
        {
            for (int i = 0; i < restrictions.Length; i++)
            {
                restrictions[i].UpdateGoal(_target);
            }
        }
    }
}