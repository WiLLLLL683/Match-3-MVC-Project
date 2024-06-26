﻿using System;
using UnityEngine;
using Array2DEditor;
using Model.Objects;
using NaughtyAttributes;

namespace Config
{
    /// <summary>
    /// Данные об уровне
    /// </summary>
    [CreateAssetMenu(fileName = "New Level", menuName = "Config/Level/Level")]
    public class LevelSO : ScriptableObject
    {
        public Sprite icon;
        public string levelName;

        [Space]
        [Header("----------Rules----------")]
        [Space]

        public CounterConfig[] goals;
        public CounterConfig[] restrictions;

        [Space]
        [Header("----------GameBoard configuration----------")]
        [Space]

        [Tooltip("Hidden Cells are used for seamlessly spawn new Blocks on top of the Gameboard")]
        public int rowsOfHiddenCells = 2;
        [InfoBox("Use Cell Type Id to configure the initial arrangement of Cells on the Gameboard")]
        public Array2DInt gameBoard;

        [Space]
        [Header("----------Block configuration----------")]
        [Space]

        public BlockTypeSetSO blockTypeSet;
        public MatchPatternSO[] matchPatterns;

        [Serializable]
        public class CounterConfig
        {
            public int count;
            public ACounterTargetSO target;
        }
    }
}
