using Model.Objects;
using UnityEngine;
using Utils;
using View;

namespace Presenter
{
    public class PausePresenter : IPausePresenter
    {
        public class Factory : AFactory<PlayerSettings, APauseView, IPausePresenter>
        {
            private readonly AFactory<PlayerSettings, APausePopUp, IPopUpPresenter> popUpFactory;
            private readonly AInput input;

            public Factory(APauseView viewPrefab,
                AFactory<PlayerSettings, APausePopUp, IPopUpPresenter> popUpFactory,
                AInput input) : base(viewPrefab)
            {
                this.popUpFactory = popUpFactory;
                this.input = input;
            }

            public override IPausePresenter Connect(APauseView existingView, PlayerSettings model)
            {
                var presenter = new PausePresenter(model, existingView, popUpFactory, input);
                presenter.Enable();
                allPresenters.Add(presenter);
                return presenter;
            }
        }

        private readonly PlayerSettings model;
        private readonly APauseView view;
        private readonly AFactory<PlayerSettings, APausePopUp, IPopUpPresenter> popUpFactory;
        private readonly AInput input;

        private IPopUpPresenter presenter;

        public PausePresenter(PlayerSettings model,
            APauseView view,
            AFactory<PlayerSettings, APausePopUp, IPopUpPresenter> popUpFactory,
            AInput input)
        {
            this.model = model;
            this.view = view;
            this.popUpFactory = popUpFactory;
            this.input = input;
        }

        public void Enable()
        {
            presenter = popUpFactory.Connect(view.PausePopUp, model);

            view.PausePopUp.OnShow += DisableInput;
            view.PausePopUp.OnHide += EnableInput;

            Debug.Log($"{this.GetType().Name} enabled");
        }
        public void Disable()
        {
            view.PausePopUp.OnShow -= DisableInput;
            view.PausePopUp.OnHide -= EnableInput;

            presenter.Disable();
            Debug.Log($"{this.GetType().Name} disabled");
        }
        public void Destroy()
        {
            Disable();
            GameObject.Destroy(view.gameObject);
        }

        private void EnableInput() => input.Enable();
        private void DisableInput() => input.Disable();
    }
}