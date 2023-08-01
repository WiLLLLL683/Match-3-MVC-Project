using Model.Infrastructure;
using Model.Readonly;
using System;
using System.Collections.Generic;
using UnityEngine;
using Utils;
using View;

namespace Presenter
{
    public class PausePopUpPresenter: IPopUpPresenter
    {
        /// <summary>
        /// Реализация фабрики использующая класс презентера в котором находится.
        /// </summary>
        public class Factory : AFactory<PlayerSettings, APausePopUp, IPopUpPresenter>
        {
            private readonly Bootstrap bootstrap;
            public Factory(APausePopUp viewPrefab, Bootstrap bootstrap, Transform parent = null) : base(viewPrefab)
            {
                this.bootstrap = bootstrap;
            }

            public override IPopUpPresenter Connect(APausePopUp existingView, PlayerSettings model)
            {
                var presenter = new PausePopUpPresenter(model, existingView, bootstrap);
                existingView.Init(model.IsSoundOn, model.IsVibrationOn);
                presenter.Enable();
                allPresenters.Add(presenter);
                return presenter;
            }
        }
        
        private readonly APausePopUp view;
        private readonly PlayerSettings model;
        private readonly Bootstrap bootstrap;

        public PausePopUpPresenter(PlayerSettings model, APausePopUp view, Bootstrap bootstrap)
        {
            this.model = model;
            this.view = view;
            this.bootstrap = bootstrap;
        }

        public void Enable()
        {
            view.OnNextLevelInput += LoadNextLevel;
            view.OnReplayInput += Replay;
            view.OnQuitInput += Quit;
            view.OnSoundIsOn += SwitchSound;
            view.OnVibrationIsOn += SwitchVibration;
            Debug.Log($"{this.GetType().Name} enabled");
        }
        public void Disable()
        {
            view.OnNextLevelInput -= LoadNextLevel;
            view.OnReplayInput -= Replay;
            view.OnQuitInput -= Quit;
            view.OnSoundIsOn -= SwitchSound;
            view.OnVibrationIsOn -= SwitchVibration;
            Debug.Log($"{this.GetType().Name} disabled");
        }
        public void Destroy()
        {
            Disable();
            GameObject.Destroy(view.gameObject);
        }

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
        private void SwitchSound(bool isOn) => model.IsSoundOn = isOn;
        private void SwitchVibration(bool isOn) => model.IsSoundOn = isOn;
    }
}