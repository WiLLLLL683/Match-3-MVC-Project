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

        public Level(LevelData levelData)
        {
            gameBoard = new GameBoard(levelData.gameBoard);
            //TODO загрузка данных уровня
        }

        public Level(int xLength, int yLength, LevelData levelData)
        {
            gameBoard = new GameBoard(xLength, yLength);
        }

    }
}