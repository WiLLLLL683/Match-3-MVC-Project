using Model.Objects;
using System.Collections.Generic;
using UnityEngine;
using View;

namespace Presenter
{
    public class BoosterFactory : FactoryBase<IBooster, IBoosterView>
    {
        private List<IBoosterPresenter> allBoosters = new();

        public BoosterFactory(IBoosterView viewPrefab, Transform parent = null) : base(viewPrefab, parent)
        {
        }

        public override IBoosterView Create(IBooster model)
        {
            IBoosterView view = GameObject.Instantiate(viewPrefab, parent);
            IBoosterPresenter presenter = new BoosterPresenter(view, model);
            presenter.Init();
            view.Init(model.Icon, model.Amount);
            allBoosters.Add(presenter);
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