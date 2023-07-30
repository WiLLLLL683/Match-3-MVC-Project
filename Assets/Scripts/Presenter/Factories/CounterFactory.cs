using Model.Readonly;
using System.Collections.Generic;
using UnityEngine;
using View;

namespace Presenter
{
    public class CounterFactory : AFactory<ICounter_Readonly, ICounterView, ICounterPresenter>
    {
        public CounterFactory(ICounterView viewPrefab, Transform parent = null) : base(viewPrefab, parent)
        {
        }

        public override ICounterPresenter Connect(ICounterView existingView, ICounter_Readonly model)
        {
            var presenter = new CounterPresenter();
            presenter.Enable();
            existingView.Init(model.Target.Icon, model.Count);
            allPresenters.Add(presenter);
            return presenter;
        }

        public override ICounterView CreateView(ICounter_Readonly model, out ICounterPresenter presenter)
        {
            ICounterView view = GameObject.Instantiate(viewPrefab, parent);
            presenter = Connect(view, model);
            return view;
        }
    }
}