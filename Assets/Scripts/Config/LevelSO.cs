using System;
using UnityEngine;
using Array2DEditor;
using Model.Objects;
using NaughtyAttributes;

namespace Config
{
    /// <summary>
    /// Данные об уровне
    /// </summary>
    [CreateAssetMenu(fileName ="New Level", menuName ="Config/Level")]
    public class LevelSO : ScriptableObject
    {
        public Sprite icon;
        public string levelName;
        [Header("-----Cell configuration-----")]
        public CellConfig cellConfig;
        [Header("-----Block configuration-----")]
        public BlockConfig blockConfig;
        [Header("-----Overall rules-----")]
        public Counter[] goals;
        public Counter[] restrictions;

        [Serializable]
        public class CellConfig
        {
            public CellTypeSO[] cellTypes;
            [InfoBox("Use index of Cell Types to configure the initial arrangement of Cells on the Gameboard")]
            public Array2DInt gameBoard;
            [InfoBox("Invisible Cells are used for seamlessly spawn new Blocks on top of the Gameboard")]
            public int rowsOfInvisibleCells;
        }

        [Serializable]
        public class BlockConfig
        {
            public BalanceSO balance;
            public PatternSO[] matchPatterns;
            public HintPatternSO[] hintPatterns;
        }
    }
}
