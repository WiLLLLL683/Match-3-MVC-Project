using Model.Objects;
using Model.Readonly;
using UnityEngine;
using View;

namespace Presenter
{
    public class BoosterInventoryFactory : AFactory<BoosterInventory, ABoosterInventoryView, IBoosterInventoryPresenter>
    {
        private readonly AFactory<IBooster, IBoosterView, IBoosterPresenter> factory;
        public BoosterInventoryFactory(ABoosterInventoryView viewPrefab, 
            AFactory<IBooster, IBoosterView, IBoosterPresenter> factory, 
            Transform parent = null) : base(viewPrefab, parent)
        {
            this.factory = factory;
        }

        public override ABoosterInventoryView Create(BoosterInventory model, out IBoosterInventoryPresenter presenter)
        {
            ABoosterInventoryView view = Object.Instantiate(viewPrefab, parent);
            presenter = new BoosterInventoryPresenter(model, view, factory);
            presenter.Enable();
            allPresenters.Add(presenter);
            return view;
        }
    }
}