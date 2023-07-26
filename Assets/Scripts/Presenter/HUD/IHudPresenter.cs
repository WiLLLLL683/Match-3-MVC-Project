using Model.Infrastructure;
using Model.Objects;
using UnityEngine;
using View;

namespace Presenter
{
    public interface IHudPresenter : IPresenter
    {
        public GameObject gameObject { get; }

        public void Init(Game game,
            FactoryBase<Counter, ICounterView, ICounterPresenter> goalFactory,
            FactoryBase<Counter, ICounterView, ICounterPresenter> restrictionFactory);
    }
}