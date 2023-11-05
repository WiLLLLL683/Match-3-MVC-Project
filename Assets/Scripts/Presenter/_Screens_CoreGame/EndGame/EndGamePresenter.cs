using UnityEngine;
using Utils;
using View;
using Model.Objects;
using Model.Services;
using Infrastructure;

namespace Presenter
{
    public class EndGamePresenter : IEndGamePresenter
    {
        //public class Factory : AFactory<Level, AEndGameView, IEndGamePresenter>
        //{
        //    private readonly AInput input;
        //    private readonly AFactory<Level, AEndGamePopUp, IPopUpPresenter> factory;
        //    private readonly IWinLoseService winLoseService;

        //    public Factory(AEndGameView viewPrefab, AInput input, AFactory<Level, AEndGamePopUp, IPopUpPresenter> factory,
        //    IWinLoseService winLoseService) : base(viewPrefab)
        //    {
        //        this.input = input;
        //        this.factory = factory;
        //        this.winLoseService = winLoseService;
        //    }

        //    public override IEndGamePresenter Connect(AEndGameView existingView, Level model)
        //    {
        //        var presenter = new EndGamePresenter(model, existingView, input, factory, winLoseService);
        //        presenter.Enable();
        //        allPresenters.Add(presenter);
        //        return presenter;
        //    }
        //}

        private readonly Level model;
        private readonly AEndGameView view;
        private readonly AInput input;
        private readonly IWinLoseService winLoseService;
        private readonly LevelLoader levelLoader;

        public EndGamePresenter(Level model,
            AEndGameView view,
            AInput input,
            IWinLoseService winLoseService,
            LevelLoader levelLoader)
        {
            this.model = model;
            this.view = view;
            this.input = input;
            this.winLoseService = winLoseService;
            this.levelLoader = levelLoader;
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

        private void LoadNextLevel() => levelLoader.LoadNextLevel();
        private void Quit() => levelLoader.LoadMetaGame();
        private void Replay() => levelLoader.ReloadCurrentLevel();
        private void UpdateScore()
        {
            view.CompletePopUp.UpdateScore(4221, 3); //TODO брать счет из модели
            view.DefeatPopUp.UpdateScore(4221, 3); //TODO брать счет из модели
        }
    }
}