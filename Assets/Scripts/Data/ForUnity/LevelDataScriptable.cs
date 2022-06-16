using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Array2DEditor;
using Model.Objects;

namespace Data.ForUnity
{
    [CreateAssetMenu(fileName ="NewLevelData",menuName ="Data/LevelData")]
    public class LevelDataScriptable : ScriptableObject
    {
        public Array2DCellTypeEnum board;
        public CounterDataForUnity[] goals;
        public CounterDataForUnity[] restrictions;
        public BalanceData balance;

        [SerializeReference]
        [SR]
        public ICounterTarget counterTarget;

        public LevelData GetLevelData()
        {
            GameBoardData boardData = new GameBoardData(board.GridSize.x, board.GridSize.y);
            for (int i = 0; i < board.GridSize.x; i++)
            {
                for (int j = 0; j < board.GridSize.y; j++)
                {
                    boardData.cellTypes[i,j] = DataFromEnum.GetCellType(board.GetCell(i,j));
                }
            }

            CounterData[] goalsData = new CounterData[goals.Length];
            for (int i = 0; i < goals.Length; i++)
            {
                goalsData[i].target = DataFromEnum.GetCounterTarget(goals[i]);
                goalsData[i].count = goals[i].count;
            }

            CounterData[] restrictionsData = new CounterData[restrictions.Length];
            for (int i = 0; i < restrictions.Length; i++)
            {
                restrictionsData[i].target = DataFromEnum.GetCounterTarget(restrictions[i]);
                restrictionsData[i].count = restrictions[i].count;
            }

            LevelData levelData = new LevelData(boardData, goalsData, restrictionsData, balance);
            return levelData;
        } 
    }
}
