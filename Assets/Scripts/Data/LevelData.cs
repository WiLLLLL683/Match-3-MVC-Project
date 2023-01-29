using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Array2DEditor;
using Model.Objects;

namespace Data
{
    [CreateAssetMenu(fileName ="LevelData",menuName ="Data/LevelData")]
    public class LevelData : ScriptableObject
    {
        public GameBoard GameBoard => GetGameboardData();
        public Counter[] Goals => (Counter[])goals.Clone(); //TODO Возможно стоит клонировать и элементы внутри массива
        public Counter[] Restrictions => (Counter[])restrictions.Clone();
        public Balance Balance => balance.Clone();
        public Pattern[] MatchPatterns => (Pattern[])matchPatterns.Clone();
        public HintPattern[] HintPatterns => (HintPattern[])hintPatterns.Clone();

        [SerializeField] private Array2DCellTypeEnum gameBoard;
        [SerializeField] private Counter[] goals;
        [SerializeField] private Counter[] restrictions;
        [SerializeField] private Balance balance;
        [SerializeField] private Pattern[] matchPatterns;
        [SerializeField] private HintPattern[] hintPatterns;

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
    }
}
