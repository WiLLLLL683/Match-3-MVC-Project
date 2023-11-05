using UnityEngine;
using View;
using Config;
using Infrastructure;
using Model.Objects;
using Zenject;
using System;

namespace Presenter
{
    /// <summary>
    /// Контроллер для окна выбора уровня
    /// </summary>
    public class LevelSelectionPresenter : ILevelSelectionPresenter
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

        public void Enable()
        {
            view.OnStartSelected += StartSelected;
            view.OnSelectNext += SelectNext;
            view.OnSelectPrevious += SelectPrevious;

            selectedLevel = allLevels[model.CurrentLevelIndex];
            view.Init(selectedLevel.icon, selectedLevel.levelName);
            view.gameObject.SetActive(true);

            Debug.Log($"{this} enabled");
        }

        public void Disable()
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