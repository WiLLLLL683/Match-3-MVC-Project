using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Array2DEditor;
using Model.Objects;
using View;
using NaughtyAttributes;

namespace Data
{
    /// <summary>
    /// Данные об уровне
    /// </summary>
    [CreateAssetMenu(fileName ="LevelData",menuName ="Data/LevelData")]
    public class LevelData : ScriptableObject
    {
        [SerializeField] private Sprite icon;
        [SerializeField] private string levelName;

        [Header("-----Cell configuration-----")]
        [SerializeField] private CellTypeSO[] cellTypes;
        [InfoBox("Use index of Cell Types to configure the initial arrangement of Cells on the Gameboard")]
        [SerializeField] private Array2DInt gameBoard;
        [InfoBox("Invisible Cells are used for seamlessly spawn new Blocks on top of the Gameboard")]
        [SerializeField] private int rowsOfInvisibleCells;
        [SerializeField] private CellTypeSO invisibleCellType;

        [Header("-----Block configuration-----")]
        [SerializeField] private Balance balance;
        [SerializeField] private Pattern[] matchPatterns;
        [SerializeField] private HintPattern[] hintPatterns;

        [Header("-----Overall rules-----")]
        [SerializeField] private Counter[] goals;
        [SerializeField] private Counter[] restrictions;

        public Sprite Icon => icon;
        public string LevelName => levelName;
        public CellTypeSO[] CellTypes => cellTypes;
        public GameBoard GameBoard => GetGameboardData();
        public int RowsOfInvisibleCells => rowsOfInvisibleCells;
        public ICellType InvisibleCellType => invisibleCellType.cellType;
        public Balance Balance => balance.Clone();
        public Pattern[] MatchPatterns => (Pattern[])matchPatterns.Clone();
        public HintPattern[] HintPatterns => (HintPattern[])hintPatterns.Clone();
        public Counter[] Goals => (Counter[])goals.Clone(); //TODO Возможно стоит клонировать и элементы внутри массива
        public Counter[] Restrictions => (Counter[])restrictions.Clone();
        
        private GameBoard GetGameboardData()
        {
            ICellType[,] cellTypesGrid = new ICellType[gameBoard.GridSize.x, gameBoard.GridSize.y];
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

            return new GameBoard(cellTypesGrid, RowsOfInvisibleCells, InvisibleCellType);
        }
    }
}
