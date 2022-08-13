using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data;

namespace Model.Objects
{
    public class Level
    {
        public GameBoard gameBoard { get; private set; }
        public Counter[] goals { get; private set; }
        public Counter[] restrictions { get; private set; }
        public Balance balance { get; private set; }
        public Pattern[] matchPatterns { get; private set; }
        public Pattern[] hintPatterns { get; private set; }

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

            //TODO внедрить загрузку паттернов
        }

        /// <summary>
        /// for tests only
        /// </summary>
        public Level(int xLength, int yLength)
        {
            gameBoard = new GameBoard(xLength, yLength);
        }

        /// <summary>
        /// for tests only
        /// </summary>
        public Level(int xLength, int yLength, Pattern[] _matchPatterns)
        {
            gameBoard = new GameBoard(xLength, yLength);
            matchPatterns = _matchPatterns;
        }
    }
}