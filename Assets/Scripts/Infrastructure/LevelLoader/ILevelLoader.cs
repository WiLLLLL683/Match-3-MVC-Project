using Config;

namespace Infrastructure
{
    public interface ILevelLoader
    {
        void LoadLevel(int levelIndex);
        void LoadMetaGame();
        void LoadNextLevel();
        void ReloadCurrentLevel();
    }
}