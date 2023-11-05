using Infrastructure;
using Model.Objects;
using UnityEngine;
using View;

namespace Presenter
{
    public class PausePresenter : IPausePresenter
    {
        //public class Factory : AFactory<PlayerSettings, APauseView, IPausePresenter>
        //{
        //    private readonly AFactory<PlayerSettings, APausePopUp, IPopUpPresenter> popUpFactory;
        //    private readonly AInput input;

        //    public Factory(APauseView viewPrefab,
        //        AFactory<PlayerSettings, APausePopUp, IPopUpPresenter> popUpFactory,
        //        AInput input) : base(viewPrefab)
        //    {
        //        this.popUpFactory = popUpFactory;
        //        this.input = input;
        //    }

        //    public override IPausePresenter Connect(APauseView existingView, PlayerSettings model)
        //    {
        //        var presenter = new PausePresenter(model, existingView, popUpFactory, input);
        //        presenter.Enable();
        //        allPresenters.Add(presenter);
        //        return presenter;
        //    }
        //}

        private readonly PlayerSettings model;
        private readonly APauseView view;
        private readonly AInput input;
        private readonly LevelLoader levelLoader;

        public PausePresenter(PlayerSettings model,
            APauseView view,
            AInput input,
            LevelLoader sceneLoader)
        {
            this.model = model;
            this.view = view;
            this.input = input;
            this.levelLoader = sceneLoader;
        }

        public void Enable()
        {
            view.PausePopUp.OnShow += DisableInput;
            view.PausePopUp.OnHide += EnableInput;
            view.PausePopUp.OnNextLevelInput += LoadNextLevel;
            view.PausePopUp.OnReplayInput += Replay;
            view.PausePopUp.OnQuitInput += Quit;
            view.PausePopUp.OnSoundIsOn += SwitchSound;
            view.PausePopUp.OnVibrationIsOn += SwitchVibration;

            Debug.Log($"{this.GetType().Name} enabled");
        }

        public void Disable()
        {
            view.PausePopUp.OnShow -= DisableInput;
            view.PausePopUp.OnHide -= EnableInput;
            view.PausePopUp.OnNextLevelInput -= LoadNextLevel;
            view.PausePopUp.OnReplayInput -= Replay;
            view.PausePopUp.OnQuitInput -= Quit;
            view.PausePopUp.OnSoundIsOn -= SwitchSound;
            view.PausePopUp.OnVibrationIsOn -= SwitchVibration;

            Debug.Log($"{this.GetType().Name} disabled");
        }

        private void EnableInput() => input.Enable();
        private void DisableInput() => input.Disable();
        private void LoadNextLevel() => levelLoader.LoadNextLevel();
        private void Quit() => levelLoader.LoadMetaGame();
        private void Replay() => levelLoader.ReloadCurrentLevel();
        private void SwitchSound(bool isOn) => model.IsSoundOn = isOn;
        private void SwitchVibration(bool isOn) => model.IsSoundOn = isOn;
    }
}