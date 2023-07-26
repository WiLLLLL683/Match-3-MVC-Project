using Model.Objects;
using Model.Readonly;
using UnityEngine;
using View;

namespace Presenter
{
    public class BoosterInventoryFactory : FactoryBase<BoosterInventory, BoosterInventoryViewBase, IBoosterInventoryPresenter>
    {
        private readonly FactoryBase<IBooster, IBoosterView, IBoosterPresenter> factory;
        public BoosterInventoryFactory(BoosterInventoryViewBase viewPrefab, 
            FactoryBase<IBooster, IBoosterView, IBoosterPresenter> factory, 
            Transform parent = null) : base(viewPrefab, parent)
        {
            this.factory = factory;
        }

        public override BoosterInventoryViewBase Create(BoosterInventory model, out IBoosterInventoryPresenter presenter)
        {
            BoosterInventoryViewBase view = Object.Instantiate(viewPrefab, parent);
            presenter = new BoosterInventoryPresenter(model, view, factory);
            presenter.Enable();
            allPresenters.Add(presenter);
            return view;
        }
    }
}