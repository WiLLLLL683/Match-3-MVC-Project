using Infrastructure;
using UnityEngine;
using Utils;
using View;
using Model.Objects;

namespace Presenter
{
    public class EndGamePopUpPresenter : IPopUpPresenter
    {
        /// <summary>
        /// Реализация фабрики использующая класс презентера в котором находится.
        /// </summary>
        public class Factory : AFactory<Level, AEndGamePopUp, IPopUpPresenter>
        {
            private readonly SceneLoader sceneLoader;
            public Factory(AEndGamePopUp viewPrefab, SceneLoader sceneLoader, Transform parent = null) : base(viewPrefab)
            {
                this.sceneLoader = sceneLoader;
            }

            public override IPopUpPresenter Connect(AEndGamePopUp existingView, Level model)
            {
                var presenter = new EndGamePopUpPresenter(model, existingView, sceneLoader);
                presenter.Enable();
                allPresenters.Add(presenter);
                return presenter;
            }
        }

        private readonly Level model;
        private readonly AEndGamePopUp view;
        private readonly SceneLoader sceneLoader;

        public EndGamePopUpPresenter(Level model, AEndGamePopUp view, SceneLoader sceneLoader)
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
            view.OnShow += UpdateScore;
            Debug.Log($"{this.GetType().Name} enabled");
        }
        public void Disable()
        {
            view.OnNextLevelInput -= LoadNextLevel;
            view.OnReplayInput -= Replay;
            view.OnQuitInput -= Quit;
            view.OnShow -= UpdateScore;
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
        private void UpdateScore() => view.UpdateScore(4221, 3); //TODO брать счет из модели
    }
}