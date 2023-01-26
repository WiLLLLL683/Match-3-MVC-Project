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
        public CounterData[] goals;
        public CounterData[] restrictions;
        public BalanceData balance;
        public Array2DBlockTypeEnum[] matchPatterns;
        public Array2DBlockTypeEnum[] hintPatterns;

        public LevelData GetLevelData()
        {
            GameBoardData boardData = GetGameboardData();

            //TODO временная заглушка
            var match = new PatternData[1];
            var hint = new PatternData[1];

            LevelData levelData = new LevelData(boardData, goals, restrictions, balance, match, hint);
            return levelData;
        }


        private GameBoardData GetGameboardData()
        {
            GameBoardData boardData = new GameBoardData(board.GridSize.x, board.GridSize.y);
            for (int i = 0; i < board.GridSize.x; i++)
            {
                for (int j = 0; j < board.GridSize.y; j++)
                {
                    boardData.cellTypes[i, j] = DataFromEnum.GetCellType(board.GetCell(i, j));
                }
            }

            return boardData;
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
