using Infrastructure;
using Model.Objects;
using Model.Services;
using UnityEngine;
using Utils;
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
        private readonly IGameBoardInput input;
        private readonly IWinLoseService winLoseService;
        private readonly IStateMachine stateMachine;

        public EndGamePresenter(Game model,
            IEndGameView view,
            IGameBoardInput input,
            IWinLoseService winLoseService,
            IStateMachine stateMachine)
        {
            this.model = model;
            this.view = view;
            this.input = input;
            this.winLoseService = winLoseService;
            this.stateMachine = stateMachine;
        }

        public void Enable()
        {
            winLoseService.OnLose += ShowDefeatPopUp;
            winLoseService.OnWin += ShowCompletePopUp;
            view.CompletePopUp.OnNextLevelInput += LoadNextLevel;
            view.CompletePopUp.OnReplayInput += Replay;
            view.CompletePopUp.OnQuitInput += Quit;
            view.DefeatPopUp.OnNextLevelInput += LoadNextLevel;
            view.DefeatPopUp.OnReplayInput += Replay;
            view.DefeatPopUp.OnQuitInput += Quit;

            Debug.Log($"{this} enabled");
        }

        public void Disable()
        {
            winLoseService.OnLose -= ShowDefeatPopUp;
            winLoseService.OnWin -= ShowCompletePopUp;
            view.CompletePopUp.OnNextLevelInput -= LoadNextLevel;
            view.CompletePopUp.OnReplayInput -= Replay;
            view.CompletePopUp.OnQuitInput -= Quit;
            view.DefeatPopUp.OnNextLevelInput -= LoadNextLevel;
            view.DefeatPopUp.OnReplayInput -= Replay;
            view.DefeatPopUp.OnQuitInput -= Quit;

            Debug.Log($"{this} disabled");
        }

        public void ShowCompletePopUp()
        {
            input.Disable();

            view.DefeatPopUp.Hide();
            view.CompletePopUp.Show(4221, 3); //TODO брать счет из модели
        }

        public void ShowDefeatPopUp()
        {
            input.Disable();

            view.CompletePopUp.Hide();
            view.DefeatPopUp.Show(4221, 3); //TODO брать счет из модели
        }

        private void LoadNextLevel()
        {
            model.LevelProgress.CurrentLevelIndex++;
            stateMachine.EnterState<CleanUpState, bool>(false);
        }
        private void Quit() => stateMachine.EnterState<CleanUpState, bool>(true);
        private void Replay() => stateMachine.EnterState<CleanUpState, bool>(false);
    }
}