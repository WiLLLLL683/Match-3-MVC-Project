using System;
using Model.Readonly;

namespace Model.Objects
{
    public class LevelProgress : ILevelProgress_Readonly
    {
        public int CurrentLevelIndex => currentLevelIndex;

        private readonly int maxLevelIndex;
        private int currentLevelIndex;

        public LevelProgress(int maxLevelIndex)
        {
            this.maxLevelIndex = maxLevelIndex;
            this.currentLevelIndex = 0; //TODO загрузка сохранения
        }

        public void SetCurrentLevel(int index)
        {
            index = Math.Clamp(index, 0, maxLevelIndex);
            currentLevelIndex = index;
            //TODO save game
        }

        public void IncrementLevelIndex() => SetCurrentLevel(currentLevelIndex + 1);
    }
}