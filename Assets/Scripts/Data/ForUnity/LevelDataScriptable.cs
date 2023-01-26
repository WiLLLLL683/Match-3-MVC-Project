using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Array2DEditor;
using Model.Objects;

namespace Data.ForUnity
{
    [CreateAssetMenu(fileName ="NewLevelData",menuName ="Data/LevelDataOld")]
    public class LevelDataScriptable : ScriptableObject
    {
        public Array2DCellTypeEnum board;
        public Counter[] goals;
        public Counter[] restrictions;
        public Balance balance;
        public Array2DBlockTypeEnum[] matchPatterns;
        public Array2DBlockTypeEnum[] hintPatterns;

        public LevelData GetLevelData()
        {
            GameBoard gameBoard = GetGameboardData();

            //TODO временная заглушка
            var match = new Pattern[1];
            var hint = new Pattern[1];

            LevelData levelData = new LevelData(gameBoard, goals, restrictions, balance, match, hint);
            return levelData;
        }


        private GameBoard GetGameboardData()
        {
            ACellType[,] aCellTypes = new ACellType[board.GridSize.x, board.GridSize.y];
            for (int i = 0; i < board.GridSize.x; i++)
            {
                for (int j = 0; j < board.GridSize.y; j++)
                {
                    aCellTypes[i, j] = DataFromEnum.GetCellType(board.GetCell(i, j));
                }
            }

            return new GameBoard(aCellTypes);
        }

        //TODO функция получения паттерна из данных
        //private PatternData GetPatternsData(Array2DBlockTypeEnum[] _data)
        //{
        //    GameBoardData boardData = new GameBoardData(board.GridSize.x, board.GridSize.y);
        //    for (int i = 0; i < board.GridSize.x; i++)
        //    {
        //        for (int j = 0; j < board.GridSize.y; j++)
        //        {
        //            boardData.cellTypes[i, j] = DataFromEnum.GetCellType(board.GetCell(i, j));
        //        }
        //    }

        //    return boardData;
        //}
    }
}
