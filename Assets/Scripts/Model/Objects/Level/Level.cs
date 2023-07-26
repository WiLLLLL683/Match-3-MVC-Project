using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data;
using Model.Systems;
using Model.Readonly;

namespace Model.Objects
{
    /// <summary>
    /// Объект уровня с игровой доской и правилами
    /// </summary>
    public class Level : ILevel_Readonly
    {
        public GameBoard gameBoard { get; private set; }
        public IGameBoard_Readonly GameBoard_Readonly => gameBoard;
        public Counter[] goals { get; private set; }
        public ICounter_Readonly[] Goals_Readonly => goals;
        public Counter[] restrictions { get; private set; }
        public ICounter_Readonly[] Restrictions_Readonly => goals;
        public Balance balance { get; private set; }
        public Pattern[] matchPatterns { get; private set; }
        public HintPattern[] hintPatterns { get; private set; }

        /// <summary>
        /// Создание уровня исходя из данных с пустым игровым полем
        /// </summary>
        public Level(LevelData levelData)
        {
            gameBoard = levelData.GameBoard;
            goals = levelData.Goals;
            restrictions = levelData.Restrictions;
            balance = levelData.Balance;
            matchPatterns = levelData.MatchPatterns;
            hintPatterns = levelData.HintPatterns;
        }

        /// <summary>
        /// for tests only
        /// </summary>
        public Level(int xLength, int yLength)
        {
            gameBoard = new GameBoard(xLength, yLength);
            goals = new Counter[1];
            goals[0] = new Counter(new BasicBlockType(), 2);
            restrictions = new Counter[1];
            restrictions[0] = new Counter(new Turn(), 2);
            matchPatterns = new Pattern[1];
            matchPatterns[0] = new Pattern(new bool[1, 1] { { true } });
            hintPatterns = new HintPattern[1];
            hintPatterns[0] = new HintPattern(new bool[1, 1] { { true } }, new(0, 0), Directions.Up);


            List<BlockType_Weight> balanceDictionary = new();
            balanceDictionary.Add(new BlockType_Weight(new BlueBlockType(), 50));
            balanceDictionary.Add(new BlockType_Weight(new RedBlockType(), 50));
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

        /// <summary>
        /// Проверить все ли цели уровня выполнены
        /// </summary>
        public bool CheckWin()
        {
            for (int i = 0; i < goals.Length; i++)
            {
                if (!goals[i].isCompleted)
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Проверить закончились ли огранияения уровня
        /// </summary>
        public bool CheckLose()
        {
            for (int i = 0; i < restrictions.Length; i++)
            {
                if (restrictions[i].isCompleted)
                    return true;
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
                goals[i].UpdateGoal(_target);
            }
        }

        /// <summary>
        /// Пересчет счетчика ограничений уровня, с вычетом 1 цели
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