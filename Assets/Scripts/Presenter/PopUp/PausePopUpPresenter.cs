using Infrastructure;
using UnityEngine;
using Model.Objects;
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
            private readonly SceneLoader sceneLoader;
            public Factory(APausePopUp viewPrefab, SceneLoader sceneLoader, Transform parent = null) : base(viewPrefab)
            {
                this.sceneLoader = sceneLoader;
            }

            public override IPopUpPresenter Connect(APausePopUp existingView, PlayerSettings model)
            {
                var presenter = new PausePopUpPresenter(model, existingView, sceneLoader);
                existingView.Init(model.IsSoundOn, model.IsVibrationOn);
                presenter.Enable();
                allPresenters.Add(presenter);
                return presenter;
            }
        }
        
        private readonly APausePopUp view;
        private readonly PlayerSettings model;
        private readonly SceneLoader sceneLoader;

        public PausePopUpPresenter(PlayerSettings model, APausePopUp view, SceneLoader sceneLoader)
        {
            this.model = model;
            this.view = view;
            this.sceneLoader = sceneLoader;
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
        private void Quit() => sceneLoader.LoadMetaGame();
        private void Replay()
        {
            //TODO replay
            Debug.Log("Replay");
        }
        private void SwitchSound(bool isOn) => model.IsSoundOn = isOn;
        private void SwitchVibration(bool isOn) => model.IsSoundOn = isOn;
    }
}