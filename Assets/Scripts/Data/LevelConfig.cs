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
            public BalanceSO balance;
            public Pattern[] matchPatterns;
            public HintPattern[] hintPatterns;
        }
    }
}
