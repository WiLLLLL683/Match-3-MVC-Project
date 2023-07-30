using Model.Infrastructure;
using System;
using UnityEngine;
using View;

namespace Presenter
{
    public class PauseScreen : APauseScreen
    {
        [SerializeField] private APausePopUp pausePopUp;

        private PlayerSettings settings;
        private IInput input;
        private Bootstrap bootstrap;

        public override void Init(PlayerSettings settings, IInput input, Bootstrap bootstrap)
        {
            this.settings = settings;
            this.input = input;
            this.bootstrap = bootstrap;

            pausePopUp.Init(this.settings.IsSoundOn, this.settings.IsVibrationOn);
        }
        public override void Enable()
        {
            pausePopUp.OnShow += DisableInput;
            pausePopUp.OnHide += EnableInput;
            pausePopUp.OnNextLevelInput += LoadNextLevel;
            pausePopUp.OnReplayInput += Replay;
            pausePopUp.OnQuitInput += Quit;
            Debug.Log($"{this} enabled", this);
        }
        public override void Disable()
        {
            pausePopUp.OnShow -= DisableInput;
            pausePopUp.OnHide -= EnableInput;
            pausePopUp.OnNextLevelInput -= LoadNextLevel;
            pausePopUp.OnReplayInput -= Replay;
            pausePopUp.OnQuitInput -= Quit;
            Debug.Log($"{this} disabled", this);
        }
        //public void Destroy()
        //{
        //    Disable();
        //    Destroy(gameObject);
        //}

        private void EnableInput() => input.Enable();
        private void DisableInput() => input.Disable();
        private void LoadNextLevel()
        {
            //TODO next level
            Debug.Log("Next Level");
        }
        private void Quit() => bootstrap.LoadMetaGame();
        private void Replay()
        {
            //TODO replay
            Debug.Log("Replay");
        }
    }
}