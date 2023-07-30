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
    public class BoosterInventoryScreen : ABoosterInventoryScreen
    {
        public class Factory
        {
            public void Create(BoosterInventoryScreen prefab, 
                BoosterInventory model,
                AFactory<IBooster, IBoosterView, IBoosterPresenter> factory)
            {
                var boosterInventory = GameObject.Instantiate(prefab);
                boosterInventory.Init(model,factory);
                boosterInventory.Enable();
            }
        }

        [SerializeField] private Transform boostersParent;
        
        private BoosterInventory model;
        private AFactory<IBooster, IBoosterView, IBoosterPresenter> factory;

        public override void Init(BoosterInventory model,
            AFactory<IBooster, IBoosterView, IBoosterPresenter> factory)
        {
            this.model = model;
            this.factory = factory;
            this.factory.SetParent(this.boostersParent);
        }
        public override void Enable()
        {
            //TODO
            Debug.Log($"{this} enabled", this);
        }
        public override void Disable()
        {
            //TODO
            Debug.Log($"{this} disabled", this);
        }



        private void SpawnBoosters()
        {
            //TODO
        }
    }
}
