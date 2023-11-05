using Model.Services;
using UnityEngine;
using View;

namespace Presenter
{
    /// <summary>
    /// Перезентер инвентаря бустеров в кор-игре
    /// </summary>
    public class BoosterInventoryPresenter : IBoosterInventoryPresenter
    {
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
