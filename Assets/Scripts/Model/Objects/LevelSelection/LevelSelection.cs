using Data;
using Model.Readonly;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Model.Objects
{
    public class LevelSelection : ILevelSelection_Readonly
    {
        public LevelData[] AllLevels { get; private set; }
        public int CurrentLevelIndex { get; private set; }
        public LevelData CurrentLevelData => AllLevels[CurrentLevelIndex];

        public LevelSelection(LevelData[] allLevels, int currentLevelIndex)
        {
            AllLevels = allLevels;
            SetCurrentLevel(currentLevelIndex);
        }

        public void SetCurrentLevel(int index)
        {
            index = Math.Clamp(index, 0, AllLevels.Length);
            CurrentLevelIndex = index;
        }
    }
}