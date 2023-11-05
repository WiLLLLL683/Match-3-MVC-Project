using UnityEngine;
using View;
using Utils;
using Model.Services;
using Model.Objects;

namespace Presenter
{
    /// <summary>
    /// Контроллер для инвентаря бустеров
    /// </summary>
    public class BoosterInventoryPresenter : IBoosterInventoryPresenter
    {
        //public class Factory : AFactory<IBoosterService, ABoosterInventoryView, IBoosterInventoryPresenter>
        //{
        //    private readonly AFactory<IBooster, ABoosterView, IBoosterPresenter> boosterFactory;

        //    public Factory(ABoosterInventoryView viewPrefab,
        //        AFactory<IBooster, ABoosterView, IBoosterPresenter> boosterFactory) : base(viewPrefab)
        //    {
        //        this.boosterFactory = boosterFactory;
        //    }

        //    public override IBoosterInventoryPresenter Connect(ABoosterInventoryView existingView, IBoosterService model)
        //    {
        //        var presenter = new BoosterInventoryPresenter(model, existingView, boosterFactory);
        //        presenter.Enable();
        //        allPresenters.Add(presenter);
        //        return presenter;
        //    }
        //}

        private readonly IBoosterService model;
        private readonly ABoosterInventoryView view;
        private readonly BoosterPresenter.Factory presenterFactory;
        private readonly BoosterView.Factory viewFactory;

        public BoosterInventoryPresenter(IBoosterService model,
            ABoosterInventoryView view,
            BoosterPresenter.Factory presenterFactory,
            BoosterView.Factory viewFactory)
        {
            this.model = model;
            this.view = view;
            this.presenterFactory = presenterFactory;
            this.viewFactory = viewFactory;
        }
        public void Enable()
        {
            //TODO
            Debug.Log($"{this} enabled");
        }
        public void Disable()
        {
            //TODO
            Debug.Log($"{this} disabled");
        }
        public void Destroy()
        {
            Disable();
            GameObject.Destroy(view.gameObject);
        }


        private void SpawnBoosters()
        {
            //TODO
        }
    }
}
