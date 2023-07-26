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
    public class BoosterInventoryPresenter : MonoBehaviour, IBoosterInventoryPresenter
    {
        [SerializeField] private Transform boostersParent;

        private FactoryBase<IBooster, IBoosterView, IBoosterPresenter> factory;

        public void Init(Game game, FactoryBase<IBooster, IBoosterView, IBoosterPresenter> factory)
        {
            this.factory = factory;
            this.factory.SetParent(boostersParent);
        }
        public void Enable()
        {

        }
        public void Disable()
        {

        }
    }
}