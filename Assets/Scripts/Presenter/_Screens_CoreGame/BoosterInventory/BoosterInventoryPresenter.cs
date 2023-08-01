using Data;
using Model.Infrastructure;
using Model.Objects;
using UnityEngine;
using View;
using Utils;

namespace Presenter
{
    /// <summary>
    /// Контроллер для инвентаря бустеров
    /// </summary>
    public class BoosterInventoryPresenter : IBoosterInventoryPresenter
    {
        public class Factory : AFactory<BoosterInventory, ABoosterInventoryView, IBoosterInventoryPresenter>
        {
            private AFactory<IBooster, ABoosterView, IBoosterPresenter> boosterFactory;

            public Factory(ABoosterInventoryView viewPrefab,
                AFactory<IBooster, ABoosterView, IBoosterPresenter> boosterFactory) : base(viewPrefab)
            {
                this.boosterFactory = boosterFactory;
            }

            public override IBoosterInventoryPresenter Connect(ABoosterInventoryView existingView, BoosterInventory model)
            {
                var presenter = new BoosterInventoryPresenter(model, existingView, boosterFactory);
                presenter.Enable();
                allPresenters.Add(presenter);
                return presenter;
            }
        }

        private BoosterInventory model;
        private ABoosterInventoryView view;
        private AFactory<IBooster, ABoosterView, IBoosterPresenter> factory;

        public BoosterInventoryPresenter(BoosterInventory model,
            ABoosterInventoryView view,
            AFactory<IBooster, ABoosterView, IBoosterPresenter> factory)
        {
            this.model = model;
            this.view = view;
            this.factory = factory;
            this.factory.SetParent(view.BoostersParent);
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
