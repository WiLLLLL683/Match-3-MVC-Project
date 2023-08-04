using UnityEngine;
using Model.Infrastructure;
using Model.Readonly;
using View;
using Utils;

namespace Presenter
{
    public class BoosterPresenter : IBoosterPresenter
    {
        /// <summary>
        /// Реализация фабрики использующая класс презентера в котором находится.
        /// </summary>
        public class Factory : AFactory<IBooster_Readonly, ABoosterView, IBoosterPresenter>
        {
            private readonly IGame game;
            public Factory(ABoosterView viewPrefab, IGame game, Transform parent = null) : base(viewPrefab)
            {
                this.game = game;
            }

            public override IBoosterPresenter Connect(ABoosterView existingView, IBooster_Readonly model)
            {
                var presenter = new BoosterPresenter(existingView, model, game);
                existingView.Init(model.Icon, model.Amount);
                allPresenters.Add(presenter);
                presenter.Enable();
                return presenter;
            }
        }

        private ABoosterView view;
        private IBooster_Readonly model;
        private readonly IGame game;

        public BoosterPresenter(ABoosterView view, IBooster_Readonly model, IGame game)
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



        private void ChangeAmount(int amount)
        {
            if (amount > 0)
                view.EnableButton();
            else
                view.DisableButton();

            view.ChangeAmount(amount);
        }
        private void ChangeIcon(Sprite icon) => view.ChangeIcon(icon);
        private void ActivateBooster() => game.ActivateBooster(model);
    }
}