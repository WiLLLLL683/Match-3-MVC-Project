﻿using Config;
using Infrastructure;
using Model.Objects;
using System;
using UnityEngine;
using Utils;
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
        private readonly IStateMachine stateMachine;
        private readonly IConfigProvider configProvider;

        public LevelSelectionPresenter(Game model,
            ILevelSelectionView view,
            IStateMachine stateMachine,
            IConfigProvider configProvider)
        {
            this.model = model.LevelProgress;
            this.view = view;
            this.stateMachine = stateMachine;
            this.configProvider = configProvider;
        }

        public void Enable()
        {
            view.OnStartSelected += StartSelected;
            view.OnSelectNext += SelectNext;
            view.OnSelectPrevious += SelectPrevious;

            SelectLastCompleted();
            //Debug.Log($"{this} enabled");
        }

        public void Disable()
        {
            view.OnStartSelected -= StartSelected;
            view.OnSelectNext -= SelectNext;
            view.OnSelectPrevious -= SelectPrevious;

            //Debug.Log($"{this} disabled");
        }

        public void StartSelected()
        {
            bool isOpen = model.CurrentLevelIndex <= model.LastOpenedLevel;

            if (!isOpen)
            {
                view.PlayLockedAnimation();
                return;
            }

            stateMachine.EnterState<LoadLevelState>();
        }

        public void SelectPrevious() => SelectLevel(model.CurrentLevelIndex - 1);

        public void SelectNext() => SelectLevel(model.CurrentLevelIndex + 1);

        private void SelectLastCompleted() => SelectLevel(model.LastCompletedLevel + 1);

        private void SelectLevel(int index)
        {
            model.CurrentLevelIndex = Mathf.Clamp(index, 0, configProvider.LastLevelIndex);

            LevelSO selectedLevel = configProvider.GetLevelSO(model.CurrentLevelIndex);
            view.SetPreviousButtonActive(model.CurrentLevelIndex != 0);
            view.SetNextButtonActive(model.CurrentLevelIndex != configProvider.LastLevelIndex);
            view.ShowSelectedLevel(selectedLevel.icon, selectedLevel.levelName);

            bool isOpen = model.CurrentLevelIndex <= model.LastOpenedLevel;
            bool isComplete = model.CurrentLevelIndex <= model.LastCompletedLevel;

            if (isComplete)
                view.ShowCompleteMark();
            if (!isOpen)
                view.ShowLockedMark();
            if (!isComplete && isOpen)
                view.HideAllMarks();
        }
    }
}
