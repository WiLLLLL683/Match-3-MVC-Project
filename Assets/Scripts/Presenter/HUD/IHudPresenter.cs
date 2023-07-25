using Model.Infrastructure;
using Model.Objects;
using UnityEngine;
using View;

namespace Presenter
{
    public interface IHudPresenter
    {
        public GameObject gameObject { get; }
        
        void Init(Game game,
            FactoryBase<Counter, ICounterView> goalFactory,
            FactoryBase<Counter, ICounterView> restrictionFactory);
    }
}