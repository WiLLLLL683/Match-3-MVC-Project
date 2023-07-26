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
            AFactory<Counter, ICounterView, ICounterPresenter> goalFactory,
            AFactory<Counter, ICounterView, ICounterPresenter> restrictionFactory);
    }
}