using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Array2DEditor;
using Model.Objects;

namespace Data
{
    [CreateAssetMenu(fileName ="NewLevelData",menuName ="Data/LevelDataOld")]
    public class LevelDataScriptable : ScriptableObject
    {
        public GameBoard GameBoard => GetGameboardData();
        public Counter[] Goals => (Counter[])goals.Clone();
        public Counter[] Restrictions => (Counter[])restrictions.Clone();
        public Balance Balance => balance.Clone();
        public Pattern[] MatchPatterns => GetPatternsData(matchPatterns);
        public Pattern[] HintPatterns => GetPatternsData(hintPatterns);

        [SerializeField] private Array2DCellTypeEnum gameBoard;
        [SerializeField] private Counter[] goals;
        [SerializeField] private Counter[] restrictions;
        [SerializeField] private Balance balance;
        [SerializeField] private Array2DBool[] matchPatterns;
        [SerializeField] private Array2DBool[] hintPatterns;

        //public LevelData GetLevelData()
        //{
        //    GameBoard gameBoard = GetGameboardData();

        //    //TODO временная заглушка
        //    var match = new Pattern[1];
        //    var hint = new Pattern[1];

        //    LevelData levelData = new LevelData(gameBoard, goals, restrictions, balance, match, hint);
        //    return levelData;
        //}


        private GameBoard GetGameboardData()
        {
            ACellType[,] aCellTypes = new ACellType[gameBoard.GridSize.x, gameBoard.GridSize.y];
            for (int i = 0; i < gameBoard.GridSize.x; i++)
            {
                for (int j = 0; j < gameBoard.GridSize.y; j++)
                {
                    aCellTypes[i, j] = DataFromEnum.GetCellType(gameBoard.GetCell(i, j));
                }
            }

            return new GameBoard(aCellTypes);
        }

        //TODO функция получения паттерна из данных
        private Pattern[] GetPatternsData(Array2DBool[] _data)
        {
            Pattern[] patterns = new Pattern[_data.Length];
            for (int i = 0; i < patterns.Length; i++)
            {
                patterns[i] = new Pattern(_data[i].GetCells());
            }

            return patterns;
        }
    }
}
