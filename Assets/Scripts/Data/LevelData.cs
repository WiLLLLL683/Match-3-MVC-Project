using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Array2DEditor;
using Model.Objects;

namespace Data
{
    /// <summary>
    /// Данные об уровне
    /// </summary>
    [CreateAssetMenu(fileName ="LevelData",menuName ="Data/LevelData")]
    public class LevelData : ScriptableObject
    {
        public GameBoard GameBoard => GetGameboardData();
        public Model.Objects.Counter[] Goals => (Model.Objects.Counter[])goals.Clone(); //TODO Возможно стоит клонировать и элементы внутри массива
        public Model.Objects.Counter[] Restrictions => (Model.Objects.Counter[])restrictions.Clone();
        public Balance Balance => balance.Clone();
        public Pattern[] MatchPatterns => (Pattern[])matchPatterns.Clone();
        public HintPattern[] HintPatterns => (HintPattern[])hintPatterns.Clone();
        public Sprite Icon => icon;
        public string LevelName => levelName;


        [SerializeReference] public ACellType[] cellTypes;
        [SerializeField] private Array2DCellTypeEnum gameBoard;
        [SerializeField] private Model.Objects.Counter[] goals;
        [SerializeField] private Model.Objects.Counter[] restrictions;
        [SerializeField] private Balance balance;
        [SerializeField] private Pattern[] matchPatterns;
        [SerializeField] private HintPattern[] hintPatterns;
        [SerializeField] private Sprite icon;
        [SerializeField] private string levelName;

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
