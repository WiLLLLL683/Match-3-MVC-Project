using Model.Objects;
using System.Collections.Generic;
using UnityEngine;
using View;

namespace Presenter
{
    public class BoosterFactory : AFactory<IBooster, IBoosterView, IBoosterPresenter>
    {
        public BoosterFactory(IBoosterView viewPrefab, Transform parent = null) : base(viewPrefab, parent)
        {
        }

        public override IBoosterPresenter Connect(IBoosterView existingView, IBooster model)
        {
            var presenter = new BoosterPresenter(existingView, model);
            existingView.Init(model.Icon, model.Amount);
            allPresenters.Add(presenter);
            presenter.Enable();
            return presenter;
        }

        public override IBoosterView CreateView(IBooster model, out IBoosterPresenter presenter)
        {
            var view = GameObject.Instantiate(viewPrefab, parent);
            presenter = Connect(view, model);
            return view;
        }
    }
}