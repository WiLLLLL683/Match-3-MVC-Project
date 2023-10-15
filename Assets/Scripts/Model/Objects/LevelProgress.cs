using System;
using Model.Readonly;

namespace Model.Objects
{
    [Serializable]
    public class LevelProgress
    {
        public int CurrentLevelIndex;
        private readonly int maxLevelIndex;

        public LevelProgress(int maxLevelIndex)
        {
            this.maxLevelIndex = maxLevelIndex;
            this.CurrentLevelIndex = 0; //TODO загрузка сохранения
        }

        public void SetCurrentLevel(int index)
        {
            index = Math.Clamp(index, 0, maxLevelIndex);
            CurrentLevelIndex = index;
            //TODO save game
        }

        public void IncrementLevelIndex() => SetCurrentLevel(CurrentLevelIndex + 1);
    }
}