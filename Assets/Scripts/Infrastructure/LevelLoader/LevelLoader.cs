using Config;
using Model.Infrastructure;
using Model.Services;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Infrastructure
{
    public class LevelLoader : ILevelLoader
    {
        public event Action OnLoadStart; //TODO избавиться от ивента - заменить на явный порядок запуска

        private readonly ZenjectSceneLoader loader;
        private readonly ILevelConfigProvider allLevels;

        private const string META_SCENE_NAME = "Meta";
        private const string CORE_SCENE_NAME = "Core";

        private int currentLevelIndex;

        public LevelLoader(ZenjectSceneLoader loader, ILevelConfigProvider allLevels)
        {
            this.loader = loader;
            this.allLevels = allLevels;
        }

        public void LoadMetaGame()
        {
            OnLoadStart?.Invoke();
            loader.LoadSceneAsync(META_SCENE_NAME);
        }
        public void ReloadCurrentLevel() => LoadLevel(currentLevelIndex);
        public void LoadNextLevel() => LoadLevel(currentLevelIndex + 1);
        public void LoadLevel(int levelIndex)
        {
            if (levelIndex > allLevels.LastLevelIndex)
                return;

            OnLoadStart?.Invoke();
            currentLevelIndex = levelIndex;
            LevelSO currentLevel = allLevels.GetSO(currentLevelIndex);
            Debug.Log($"Loading level: {currentLevel.levelName}");

            loader.LoadSceneAsync(CORE_SCENE_NAME, LoadSceneMode.Single, (context) => //TODO вынести в сервис?
            {
                //передача конфига уровня в кор-сцену
                context.BindInstance(currentLevel).AsSingle();
                context.BindInstance(currentLevel.blockTypeSet).AsSingle();
            });
        }
    }
}