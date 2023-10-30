using UnityEngine;
using View;
using Config;
using Model.Objects;
using Zenject;
using System;

namespace Presenter
{
    /// <summary>
    /// Контроллер для окна выбора уровня
    /// </summary>
    public class LevelSelectionPresenter : ILevelSelectionPresenter, IInitializable, IDisposable
    {
        private readonly LevelProgress model;
        private readonly ALevelSelectionView view;
        private readonly SceneLoader sceneLoader;
        private readonly LevelSO[] allLevels;

        private LevelSO selectedLevel;

        public LevelSelectionPresenter(LevelProgress model, ALevelSelectionView view, SceneLoader sceneLoader, LevelSO[] allLevels)
        {
            this.model = model;
            this.view = view;
            this.sceneLoader = sceneLoader;
            this.allLevels = allLevels;
        }

        public void Initialize()
        {
            view.OnStartSelected += StartSelected;
            view.OnSelectNext += SelectNext;
            view.OnSelectPrevious += SelectPrevious;

            selectedLevel = allLevels[model.CurrentLevelIndex];
            view.Init(selectedLevel.icon, selectedLevel.levelName);
            view.gameObject.SetActive(true);

            Debug.Log($"{this} enabled");
        }

        public void Dispose()
        {
            view.OnStartSelected -= StartSelected;
            view.OnSelectNext -= SelectNext;
            view.OnSelectPrevious -= SelectPrevious;

            Debug.Log($"{this} disabled");
        }

        public void SelectPrevious()
        {
            //TODO
        }

        public void SelectNext()
        {
            //TODO
        }

        public void StartSelected() => sceneLoader.LoadCoreGame(selectedLevel);
    }
}