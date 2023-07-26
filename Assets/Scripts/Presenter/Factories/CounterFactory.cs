using Model.Objects;
using Model.Readonly;
using System.Collections.Generic;
using UnityEngine;
using View;

namespace Presenter
{
    public class CounterFactory : AFactory<Counter, ICounterView, ICounterPresenter>
    {
        public CounterFactory(ICounterView viewPrefab, Transform parent = null) : base(viewPrefab, parent)
        {
        }

        public override ICounterView Create(Counter model, out ICounterPresenter presenter)
        {
            ICounterView view = GameObject.Instantiate(viewPrefab, parent);
            presenter = new CounterPresenter();
            presenter.Enable();
            view.Init(model.Target.Icon, model.Count);
            allPresenters.Add(presenter);
            return view;
        }
    }
}