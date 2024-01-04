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
        private readonly IBoostersView view;
        private readonly BoosterPresenter.Factory presenterFactory;
        private readonly BoosterButtonView.Factory viewFactory;

        public BoosterInventoryPresenter(IBoosterService model,
            IBoostersView view,
            BoosterPresenter.Factory presenterFactory,
            BoosterButtonView.Factory viewFactory)
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

        private void SpawnBoosters()
        {
            //TODO
        }
    }
}
