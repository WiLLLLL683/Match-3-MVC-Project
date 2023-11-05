using Config;
using Infrastructure;
using Model.Objects;
using System;
using UnityEngine;
using View;

namespace Presenter
{
    /// <summary>
    /// Презентер для окна выбора уровня
    /// Отображает выбранный уровень
    /// Передает инпут для выбора уровня и запуска выбранного уровня
    /// </summary>
    public class LevelSelectionPresenter : ILevelSelectionPresenter
    {
        private readonly LevelProgress model;
        private readonly ALevelSelectionView view;
        private readonly LevelLoader sceneLoader;
        private readonly LevelSO[] allLevels;

        private LevelSO SelectedLevel => allLevels[selectedLevelIndex];
        private int selectedLevelIndex;

        public LevelSelectionPresenter(LevelProgress model, ALevelSelectionView view, LevelLoader sceneLoader, LevelSO[] allLevels)
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

            bool isLevelIndexValid = SetSelectedLevelIndex(model.CurrentLevelIndex);

            if (!isLevelIndexValid)
            {
                Debug.LogError("Invalid Level Index, check AllLevels in config installer or LevelProgress in model");
            }

            UpdateView();
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
            SetSelectedLevelIndex(selectedLevelIndex - 1);
            UpdateView();
        }

        public void SelectNext()
        {
            SetSelectedLevelIndex(selectedLevelIndex + 1);
            UpdateView();
        }

        public void StartSelected() => sceneLoader.LoadLevel(selectedLevelIndex);

        private void UpdateView()
        {
            view.Init(SelectedLevel.icon, SelectedLevel.levelName);
            view.gameObject.SetActive(true);
        }

        private bool SetSelectedLevelIndex(int index)
        {
            if (index >= allLevels.Length)
                return false;

            if (index < 0)
                return false;

            selectedLevelIndex = index;
            return true;
        }
    }
}