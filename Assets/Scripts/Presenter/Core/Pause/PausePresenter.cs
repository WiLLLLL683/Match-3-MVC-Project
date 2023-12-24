using Infrastructure;
using Model.Objects;
using UnityEngine;
using Utils;
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
        private readonly Game model;
        private readonly IPauseView view;
        private readonly IInput input;
        private readonly IStateMachine stateMachine;

        public PausePresenter(Game model,
            IPauseView view,
            IInput input,
            IStateMachine stateMachine)
        {
            this.model = model;
            this.view = view;
            this.input = input;
            this.stateMachine = stateMachine;
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
        private void LoadNextLevel()
        {
            model.LevelProgress.CurrentLevelIndex++;
            stateMachine.EnterState<CleanUpState, bool>(false);
        }
        private void Quit() => stateMachine.EnterState<CleanUpState, bool>(true);
        private void Replay() => stateMachine.EnterState<CleanUpState, bool>(false);
        private void SwitchSound(bool isOn) => model.PlayerSettings.IsSoundOn = isOn;
        private void SwitchVibration(bool isOn) => model.PlayerSettings.IsSoundOn = isOn;
    }
}