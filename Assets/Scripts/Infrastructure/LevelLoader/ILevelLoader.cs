using Config;
using System;

namespace Infrastructure
{
    public interface ILevelLoader
    {
        event Action OnLoadStart;

        void LoadLevel(int levelIndex);
        void LoadMetaGame();
        void LoadNextLevel();
        void ReloadCurrentLevel();
    }
}