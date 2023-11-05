using UnityEngine;
using Model.Infrastructure;
using View;
using Utils;
using Model.Objects;
using Zenject;

namespace Presenter
{
    public class BoosterPresenter : IBoosterPresenter
    {
        ///// <summary>
        ///// Реализация фабрики использующая класс презентера в котором находится.
        ///// </summary>
        //public class Factory : AFactory<IBooster, ABoosterView, IBoosterPresenter>
        //{
        //    private readonly IGame game;
        //    public Factory(ABoosterView viewPrefab, IGame game, Transform parent = null) : base(viewPrefab)
        //    {
        //        this.game = game;
        //    }

        //    public override IBoosterPresenter Connect(ABoosterView existingView, IBooster model)
        //    {
        //        var presenter = new BoosterPresenter(existingView, model, game);
        //        //existingView.Init(model.Icon, model.Amount);
        //        allPresenters.Add(presenter);
        //        presenter.Enable();
        //        return presenter;
        //    }
        //}
        public class Factory : PlaceholderFactory<BoosterPresenter> { }

        private readonly ABoosterView view;
        private readonly IBooster model;
        private readonly IGame game;

        public BoosterPresenter(ABoosterView view, IBooster model, IGame game)
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