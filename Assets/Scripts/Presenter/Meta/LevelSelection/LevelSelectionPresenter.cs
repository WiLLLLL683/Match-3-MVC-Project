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
        private readonly ILevelSelectionView view;
        private readonly LevelLoader sceneLoader;
        private readonly LevelSO[] allLevels;

        private LevelSO SelectedLevel => allLevels[selectedLevelIndex];
        private int selectedLevelIndex;
        private bool IsOpen => selectedLevelIndex <= model.OpenedLevels;
        private bool IsComplete => selectedLevelIndex <= model.CompletedLevels;

        public LevelSelectionPresenter(LevelProgress model, ILevelSelectionView view, LevelLoader sceneLoader, LevelSO[] allLevels)
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

            SetSelectedLevel(model.CompletedLevels + 1); //изначально выбрать следующий за пройденным уровень
            Debug.Log($"{this} enabled");
        }

        public void Disable()
        {
            view.OnStartSelected -= StartSelected;
            view.OnSelectNext -= SelectNext;
            view.OnSelectPrevious -= SelectPrevious;

            Debug.Log($"{this} disabled");
        }

        public void SelectPrevious() => SetSelectedLevel(selectedLevelIndex - 1);
        public void SelectNext() => SetSelectedLevel(selectedLevelIndex + 1);
        public void StartSelected()
        {
            if (!IsOpen)
            {
                view.PlayLockedAnimation();
                return;
            }

            sceneLoader.LoadLevel(selectedLevelIndex);
        }

        private void SetSelectedLevel(int index)
        {
            selectedLevelIndex = Mathf.Clamp(index, 0, allLevels.Length - 1);

            view.SetPreviousButtonActive(selectedLevelIndex != 0);
            view.SetNextButtonActive(selectedLevelIndex != allLevels.Length - 1);
            view.ShowSelectedLevel(SelectedLevel.icon, SelectedLevel.levelName);

            if (IsComplete)
                view.ShowCompleteMark();
            if (!IsOpen)
                view.ShowLockedMark();
            if (!IsComplete && IsOpen)
                view.HideAllMarks();
        }
    }
}
