using Data;
using Model.Infrastructure;
using Model.Objects;
using UnityEngine;
using View;

namespace Presenter
{
    /// <summary>
    /// Контроллер для инвентаря бустеров
    /// </summary>
    public class BoosterInventoryPresenter : IBoosterInventoryPresenter
    {
        private ABoosterInventoryView view;
        private BoosterInventory model;
        private AFactory<IBooster, IBoosterView, IBoosterPresenter> factory;

        public BoosterInventoryPresenter(BoosterInventory model, 
            ABoosterInventoryView view, 
            AFactory<IBooster, IBoosterView, IBoosterPresenter> factory)
        {
            this.view = view;
            this.model = model;
            this.factory = factory;
            this.factory.SetParent(this.view.BoostersParent);
        }
        public void Enable()
        {

        }
        public void Disable()
        {

        }
        public void Destroy()
        {
            Disable();
        }
    }
}
