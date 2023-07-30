using Model.Infrastructure;
using System;
using UnityEngine;
using Utils;
using View;

namespace Presenter
{
    public class PauseScreen : APauseScreen
    {
        [SerializeField] private APausePopUp pausePopUp;

        private PlayerSettings settings;
        private Game game;
        private AFactory<PlayerSettings, APausePopUp, IPopUpPresenter> popUpFactory;
        private AInput input;
        private IPopUpPresenter presenter;

        public override void Init(PlayerSettings settings, Game game, AFactory<PlayerSettings, APausePopUp, IPopUpPresenter> popUpFactory, AInput input)
        {
            this.settings = settings;
            this.game = game;
            this.popUpFactory = popUpFactory;
            this.input = input;
        }

        public override void Enable()
        {
            presenter = popUpFactory.Connect(pausePopUp, settings);

            pausePopUp.OnShow += DisableInput;
            pausePopUp.OnHide += EnableInput;

            Debug.Log($"{this.GetType().Name} enabled", this);
        }

        public override void Disable()
        {
            pausePopUp.OnShow -= DisableInput;
            pausePopUp.OnHide -= EnableInput;

            presenter.Disable();
            Debug.Log($"{this.GetType().Name} disabled", this);
        }

        private void EnableInput() => input.Enable();
        private void DisableInput() => input.Disable();
    }
}