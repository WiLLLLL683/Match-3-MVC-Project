using Config;
using Model.Infrastructure;
using Model.Services;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Infrastructure
{
    public class LevelLoader
    {
        public LevelSO CurrentLevel => allLevels[currentLevelIndex];

        private readonly ZenjectSceneLoader loader;
        private readonly LevelSO[] allLevels;

        private const string META_SCENE_NAME = "Meta";
        private const string CORE_SCENE_NAME = "Core";

        private int currentLevelIndex;

        public LevelLoader(ZenjectSceneLoader loader, LevelSO[] allLevels)
        {
            this.loader = loader;
            this.allLevels = allLevels;
        }

        public void LoadMetaGame() => loader.LoadSceneAsync(META_SCENE_NAME);
        public void ReloadCurrentLevel() => LoadLevel(currentLevelIndex);
        public void LoadNextLevel() => LoadLevel(currentLevelIndex + 1);
        public void LoadLevel(int levelIndex)
        {
            if (levelIndex >= allLevels.Length)
                return;

            currentLevelIndex = levelIndex;
            Debug.Log($"Loading level: {CurrentLevel.levelName}");

            loader.LoadSceneAsync(CORE_SCENE_NAME, LoadSceneMode.Single, (context) =>
            {
                //передача конфига уровня в кор-сцену
                context.BindInstance(CurrentLevel).AsSingle();
                context.BindInstance(CurrentLevel.blockTypeSet).AsSingle();
            });
        }
    }
}