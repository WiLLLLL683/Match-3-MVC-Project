using Config;
using Model.Readonly;
using System;

namespace Model.Objects
{
    public class LevelSelection : ILevelSelection_Readonly
    {
        public LevelSO[] AllLevels { get; private set; }
        public int CurrentLevelIndex { get; private set; }
        public LevelSO CurrentLevelData => AllLevels[CurrentLevelIndex];

        public LevelSelection(LevelSO[] allLevels, int currentLevelIndex)
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