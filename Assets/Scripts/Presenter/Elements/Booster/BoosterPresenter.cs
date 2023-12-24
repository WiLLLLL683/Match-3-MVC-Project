using Infrastructure;
using Model.Objects;
using UnityEngine;
using Utils;
using View;
using Zenject;

namespace Presenter
{
    public class BoosterPresenter : IBoosterPresenter
    {
        public class Factory : PlaceholderFactory<BoosterPresenter> { }

        private readonly IBoosterView view;
        private readonly IBooster model;
        private readonly IStateMachine stateMachine;

        public BoosterPresenter(IBoosterView view, IBooster model, IStateMachine stateMachine)
        {
            this.view = view;
            this.model = model;
            this.stateMachine = stateMachine;
        }
        public void Enable()
        {
            view.OnActivate += ActivateBooster;
        }

        public void Disable()
        {
            view.OnActivate -= ActivateBooster;
        }

        public void Destroy()
        {
            Disable();
            GameObject.Destroy(view.gameObject);
        }

        private void ChangeIcon(Sprite icon) => view.ChangeIcon(icon);
        private void ActivateBooster() => stateMachine.EnterState<InputBoosterState, IBooster>(model);
        private void ChangeAmount(int amount)
        {
            if (amount > 0)
                view.EnableButton();
            else
                view.DisableButton();

            view.ChangeAmount(amount);
        }
    }
}