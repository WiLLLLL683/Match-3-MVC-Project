using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model.Objects
{
    public class Level
    {
        public GameBoard gameBoard { get; private set; }
        public IGoal[] goals { get; private set; }
        public IRestriction[] restrictions { get; private set; }
        public Balance balance { get; private set; }

        public Level()
        {
            gameBoard = new GameBoard(1,1);
            //TODO загрузка данных уровня
        }
    }
}