using Model.Objects;
using Model.Readonly;
using System.Collections.Generic;
using UnityEngine;
using View;

namespace Presenter
{
    public class CounterFactory : FactoryBase<Counter, ICounterView, ICounterPresenter>
    {
        private List<ICounterPresenter> allCounters = new();

        public CounterFactory(ICounterView viewPrefab, Transform parent = null) : base(viewPrefab, parent)
        {
        }

        public override ICounterView Create(Counter model, out ICounterPresenter presenter)
        {
            ICounterView view = GameObject.Instantiate(viewPrefab, parent);
            presenter = new CounterPresenter();
            presenter.Enable();
            view.Init(model.Target.Icon, model.Count);
            allCounters.Add(presenter);
            return view;
        }
        public override void Clear()
        {
            for (int i = 0; i < allCounters.Count; i++)
            {
                allCounters[i].Destroy();
            }

            allCounters.Clear();
        }
    }
}