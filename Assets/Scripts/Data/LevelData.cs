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
        public Counter[] Goals => (Counter[])goals.Clone(); //TODO Возможно стоит клонировать и элементы внутри массива
        public Counter[] Restrictions => (Counter[])restrictions.Clone();
        public Balance Balance => balance.Clone();
        public Pattern[] MatchPatterns => (Pattern[])matchPatterns.Clone();
        public HintPattern[] HintPatterns => (HintPattern[])hintPatterns.Clone();
        public Sprite Icon => icon;
        public string LevelName => levelName;
        public CellTypeSO[] CellTypes => cellTypes;


        [SerializeField] private Sprite icon;
        [SerializeField] private string levelName;
        [SerializeField] private CellTypeSO[] cellTypes;
        [SerializeField] private Array2DInt gameBoard;
        [SerializeField] private Counter[] goals;
        [SerializeField] private Counter[] restrictions;
        [SerializeField] private Balance balance;
        [SerializeField] private Pattern[] matchPatterns;
        [SerializeField] private HintPattern[] hintPatterns;

        private GameBoard GetGameboardData()
        {
            ICellType[,] cellTypesGrid = new BasicCellType[gameBoard.GridSize.x, gameBoard.GridSize.y];
            for (int i = 0; i < gameBoard.GridSize.x; i++)
            {
                for (int j = 0; j < gameBoard.GridSize.y; j++)
                {
                    int cellTypeIndex = gameBoard.GetCell(i, j);
                    if (cellTypeIndex >= cellTypes.Length)
                        cellTypeIndex = 0;

                    cellTypesGrid[i, j] = cellTypes[cellTypeIndex].cellType; //DataFromEnum.GetCellType(gameBoard.GetCell(i, j));
                }
            }

            return new GameBoard(cellTypesGrid);
        }
    }
}
