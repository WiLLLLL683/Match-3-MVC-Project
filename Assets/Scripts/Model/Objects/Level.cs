using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data;

namespace Model.Objects
{
    public class Level
    {
        public GameBoard gameBoard { get; private set; }
        public Goal[] goals { get; private set; }
        public IRestriction[] restrictions { get; private set; }
        public Balance balance { get; private set; }

        public Level(int xLength, int yLength, LevelDataScriptable levelData)
        {
            gameBoard = new GameBoard(xLength, yLength);
            //TODO загрузка данных уровня
        }
    }
}