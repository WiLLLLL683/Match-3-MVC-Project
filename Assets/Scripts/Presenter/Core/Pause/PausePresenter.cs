using Infrastructure;
using Model.Objects;
using UnityEngine;
using View;

namespace Presenter
{
    /// <summary>
    /// Презентер экрана паузы
    /// Отображает настройки игры
    /// Передает ипут для смены уровня, выхода из кор-игры, смены настроек
    /// </summary>
    public class PausePresenter : IPausePresenter
    {
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