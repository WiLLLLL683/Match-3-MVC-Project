using Model.Infrastructure;
using Model.Readonly;
using System;
using System.Collections.Generic;
using UnityEngine;
using Utils;
using View;

namespace Presenter
{
    public class EndGamePopUpPresenter : IPopUpPresenter
    {
        /// <summary>
        /// Реализация фабрики использующая класс презентера в котором находится.
        /// </summary>
        public class Factory : AFactory<ILevel_Readonly, AEndGamePopUp, IPopUpPresenter>
        {
            private readonly Bootstrap bootstrap;
            public Factory(AEndGamePopUp viewPrefab, Bootstrap bootstrap, Transform parent = null) : base(viewPrefab)
            {
                this.bootstrap = bootstrap;
            }

            public override IPopUpPresenter Connect(AEndGamePopUp existingView, ILevel_Readonly model)
            {
                var presenter = new EndGamePopUpPresenter(model, existingView, bootstrap);
                presenter.Enable();
                allPresenters.Add(presenter);
                return presenter;
            }
        }

        private readonly ILevel_Readonly model;
        private readonly AEndGamePopUp view;
        private readonly Bootstrap bootstrap;

        public EndGamePopUpPresenter(ILevel_Readonly model, AEndGamePopUp view, Bootstrap bootstrap)
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
        private void Quit() => bootstrap.LoadMetaGame();
        private void Replay()
        {
            //TODO replay
            Debug.Log("Replay");
        }
        private void UpdateScore() => view.UpdateScore(4221, 3); //TODO брать счет из модели
    }
}