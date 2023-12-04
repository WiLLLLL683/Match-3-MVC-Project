﻿using Config;
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
        private readonly ILevelLoader levelLoader;
        private readonly IConfigProvider configProvider;

        private int selectedLevelIndex;
        private LevelSO SelectedLevel => configProvider.GetSO(selectedLevelIndex);
        private bool IsOpen => selectedLevelIndex <= model.LastOpenedLevel;
        private bool IsComplete => selectedLevelIndex <= model.LastCompletedLevel;

        public LevelSelectionPresenter(LevelProgress model,
            ILevelSelectionView view,
            ILevelLoader levelLoader,
            IConfigProvider configProvider)
        {
            this.model = model;
            this.view = view;
            this.levelLoader = levelLoader;
            this.configProvider = configProvider;
        }

        public void Enable()
        {
            view.OnStartSelected += StartSelected;
            view.OnSelectNext += SelectNext;
            view.OnSelectPrevious += SelectPrevious;

            SetSelectedLevel(model.LastCompletedLevel + 1); //изначально выбрать следующий за пройденным уровень
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

            levelLoader.LoadLevel(selectedLevelIndex);
        }

        private void SetSelectedLevel(int index)
        {
            selectedLevelIndex = Mathf.Clamp(index, 0, configProvider.LastLevelIndex);

            view.SetPreviousButtonActive(selectedLevelIndex != 0);
            view.SetNextButtonActive(selectedLevelIndex != configProvider.LastLevelIndex);
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
