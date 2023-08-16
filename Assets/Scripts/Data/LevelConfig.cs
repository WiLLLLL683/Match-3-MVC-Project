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
    [CreateAssetMenu(fileName ="New Level", menuName ="Config/Level")]
    public class LevelConfig : ScriptableObject
    {
        public Sprite icon;
        public string levelName;
        public CellConfig cellConfig;
        public BlockConfig blockConfig;
        [Header("-----Overall rules-----")]
        public Counter[] goals;
        public Counter[] restrictions;

        [Serializable]
        public class CellConfig
        {
            [Header("-----Cell configuration-----")]
            public CellTypeSO[] cellTypes;
            [InfoBox("Use index of Cell Types to configure the initial arrangement of Cells on the Gameboard")]
            public Array2DInt gameBoard;
            [InfoBox("Invisible Cells are used for seamlessly spawn new Blocks on top of the Gameboard")]
            public int rowsOfInvisibleCells;
        }

        [Serializable]
        public class BlockConfig
        {
            [Header("-----Block configuration-----")]
            public Balance balance;
            public Pattern[] matchPatterns;
            public HintPattern[] hintPatterns;
        }


        //public Sprite Icon => icon;
        //public string LevelName => levelName;
        //public CellTypeSO[] CellTypes => cellTypes;
        //public GameBoard GameBoard => GetGameboardData();
        //public int RowsOfInvisibleCells => rowsOfInvisibleCells;
        //public ICellType InvisibleCellType => invisibleCellType.cellType;
        //public Balance Balance => balance.Clone();
        //public Pattern[] MatchPatterns => (Pattern[])matchPatterns.Clone();
        //public HintPattern[] HintPatterns => (HintPattern[])hintPatterns.Clone();
        //public Counter[] Goals => (Counter[])goals.Clone(); //TODO Возможно стоит клонировать и элементы внутри массива
        //public Counter[] Restrictions => (Counter[])restrictions.Clone();
        
        //private GameBoard GetGameboardData()
        //{
        //    ICellType[,] cellTypesGrid = new ICellType[gameBoard.GridSize.x, gameBoard.GridSize.y];
        //    for (int i = 0; i < gameBoard.GridSize.x; i++)
        //    {
        //        for (int j = 0; j < gameBoard.GridSize.y; j++)
        //        {
        //            int cellTypeIndex = gameBoard.GetCell(i, j);
        //            if (cellTypeIndex >= cellTypes.Length)
        //                cellTypeIndex = 0;

        //            cellTypesGrid[i, j] = cellTypes[cellTypeIndex].cellType; //DataFromEnum.GetCellType(gameBoard.GetCell(i, j));
        //        }
        //    }

        //    return new GameBoard(cellTypesGrid, RowsOfInvisibleCells, InvisibleCellType);
        //}
    }
}
