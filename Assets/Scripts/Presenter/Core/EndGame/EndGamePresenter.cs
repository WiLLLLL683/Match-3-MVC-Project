﻿using Infrastructure;
using Model.Objects;
using Model.Services;
using UnityEngine;
using View;

namespace Presenter
{
    /// <summary>
    /// Презентер экрана окончания игры
    /// Отображает победу/поражение и набранные очки
    /// Передает ипут для смены уровня, выхода из кор-игры
    /// </summary>
    public class EndGamePresenter : IEndGamePresenter
    {
        private readonly Game model;
        private readonly IEndGameView view;
        private readonly IInput input;
        private readonly IWinLoseService winLoseService;
        private readonly GameStateMachine gameStateMachine;

        public EndGamePresenter(Game model,
            IEndGameView view,
            IInput input,
            IWinLoseService winLoseService,
            GameStateMachine gameStateMachine)
        {
            this.model = model;
            this.view = view;
            this.input = input;
            this.winLoseService = winLoseService;
            this.gameStateMachine = gameStateMachine;
        }

        public void Enable()
        {
            winLoseService.OnLose += ShowDefeatPopUp;
            winLoseService.OnWin += ShowCompletePopUp;
            view.CompletePopUp.OnNextLevelInput += LoadNextLevel;
            view.CompletePopUp.OnReplayInput += Replay;
            view.CompletePopUp.OnQuitInput += Quit;
            view.CompletePopUp.OnShow += UpdateScore;
            view.DefeatPopUp.OnNextLevelInput += LoadNextLevel;
            view.DefeatPopUp.OnReplayInput += Replay;
            view.DefeatPopUp.OnQuitInput += Quit;
            view.DefeatPopUp.OnShow += UpdateScore;

            Debug.Log($"{this} enabled");
        }

        public void Disable()
        {
            winLoseService.OnLose -= ShowDefeatPopUp;
            winLoseService.OnWin -= ShowCompletePopUp;
            view.CompletePopUp.OnNextLevelInput -= LoadNextLevel;
            view.CompletePopUp.OnReplayInput -= Replay;
            view.CompletePopUp.OnQuitInput -= Quit;
            view.CompletePopUp.OnShow -= UpdateScore;
            view.DefeatPopUp.OnNextLevelInput -= LoadNextLevel;
            view.DefeatPopUp.OnReplayInput -= Replay;
            view.DefeatPopUp.OnQuitInput -= Quit;
            view.DefeatPopUp.OnShow -= UpdateScore;

            Debug.Log($"{this} disabled");
        }

        public void ShowCompletePopUp()
        {
            input.Disable();

            view.DefeatPopUp.Hide();
            view.CompletePopUp.Show();
        }

        public void ShowDefeatPopUp()
        {
            input.Disable();

            view.CompletePopUp.Hide();
            view.DefeatPopUp.Show();
        }

        private void LoadNextLevel()
        {
            model.LevelProgress.CurrentLevelIndex++;
            gameStateMachine.EnterState<CoreState>();
        }
        private void Quit() => gameStateMachine.EnterState<MetaState>();
        private void Replay() => gameStateMachine.EnterState<CoreState>();
        private void UpdateScore()
        {
            view.CompletePopUp.UpdateScore(4221, 3); //TODO брать счет из модели
            view.DefeatPopUp.UpdateScore(4221, 3); //TODO брать счет из модели
        }
    }
}