using Data;
using Model.Infrastructure;
using Model.Objects;
using UnityEngine;
using View;

namespace Presenter
{
    public interface IBoosterInventoryPresenter : IPresenter
    {
        public void Init(Game game, FactoryBase<IBooster, IBoosterView, IBoosterPresenter> factory);

        public GameObject gameObject { get; }
    }
}