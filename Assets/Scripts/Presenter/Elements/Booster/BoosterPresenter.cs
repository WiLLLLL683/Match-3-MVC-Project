using UnityEngine;
using Model.Infrastructure;
using View;
using Model.Objects;
using Zenject;

namespace Presenter
{
    public class BoosterPresenter : IBoosterPresenter
    {
        public class Factory : PlaceholderFactory<BoosterPresenter> { }

        private readonly IBoosterView view;
        private readonly IBooster model;
        private readonly IGame game;

        public BoosterPresenter(IBoosterView view, IBooster model, IGame game)
        {
            this.view = view;
            this.model = model;
            this.game = game;
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
        private void ActivateBooster() => game.ActivateBooster(model);
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