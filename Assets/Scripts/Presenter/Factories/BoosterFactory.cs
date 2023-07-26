using Model.Objects;
using System.Collections.Generic;
using UnityEngine;
using View;

namespace Presenter
{
    public class BoosterFactory : FactoryBase<IBooster, IBoosterView, IBoosterPresenter>
    {
        private List<IBoosterPresenter> allBoosters = new();

        public BoosterFactory(IBoosterView viewPrefab, Transform parent = null) : base(viewPrefab, parent)
        {
        }

        public override IBoosterView Create(IBooster model, out IBoosterPresenter presenter)
        {
            IBoosterView view = GameObject.Instantiate(viewPrefab, parent);
            presenter = new BoosterPresenter(view, model);
            view.Init(model.Icon, model.Amount);
            allBoosters.Add(presenter);
            presenter.Enable();
            return view;
        }
        public override void Clear()
        {
            for (int i = 0; i < allBoosters.Count; i++)
            {
                allBoosters[i].Destroy();
            }

            allBoosters.Clear();
        }
    }
}