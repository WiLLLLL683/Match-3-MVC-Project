using UnityEngine;
using Model.Readonly;
using View;
using Utils;

namespace Presenter
{
    /// <summary>
    /// Контроллер для инвентаря бустеров
    /// </summary>
    public class BoosterInventoryPresenter : IBoosterInventoryPresenter
    {
        public class Factory : AFactory<IBoosterService_Readonly, ABoosterInventoryView, IBoosterInventoryPresenter>
        {
            private readonly AFactory<IBooster_Readonly, ABoosterView, IBoosterPresenter> boosterFactory;

            public Factory(ABoosterInventoryView viewPrefab,
                AFactory<IBooster_Readonly, ABoosterView, IBoosterPresenter> boosterFactory) : base(viewPrefab)
            {
                this.boosterFactory = boosterFactory;
            }

            public override IBoosterInventoryPresenter Connect(ABoosterInventoryView existingView, IBoosterService_Readonly model)
            {
                var presenter = new BoosterInventoryPresenter(model, existingView, boosterFactory);
                presenter.Enable();
                allPresenters.Add(presenter);
                return presenter;
            }
        }

        private readonly IBoosterService_Readonly model;
        private readonly ABoosterInventoryView view;
        private readonly AFactory<IBooster_Readonly, ABoosterView, IBoosterPresenter> factory;

        public BoosterInventoryPresenter(IBoosterService_Readonly model,
            ABoosterInventoryView view,
            AFactory<IBooster_Readonly, ABoosterView, IBoosterPresenter> factory)
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
