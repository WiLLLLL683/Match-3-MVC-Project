using Model.Objects;
using System.Collections.Generic;
using UnityEngine;
using View;

namespace Presenter
{
    public class BoosterFactory : IFactory<IBooster, IBoosterView>
    {
        private Transform parent;
        private IBoosterView boosterPrefab;

        private List<IBoosterPresenter> allBoosters = new();

        public BoosterFactory(IBoosterView boosterPrefab, Transform parent)
        {
            this.parent = parent;
            this.boosterPrefab = boosterPrefab;
        }

        public IBoosterView Create(IBooster model)
        {
            IBoosterView view = GameObject.Instantiate(boosterPrefab, parent);
            IBoosterPresenter presenter = new BoosterPresenter(view, model);
            presenter.Init();
            view.Init(model.Icon, model.Amount);
            allBoosters.Add(presenter);
            return view;
        }
        public void Clear()
        {
            for (int i = 0; i < allBoosters.Count; i++)
            {
                allBoosters[i].Destroy();
            }

            allBoosters.Clear();

            //уничтожить неучтенные объекты
            for (int i = 0; i < parent.childCount; i++)
            {
                GameObject.Destroy(parent.GetChild(i).gameObject);
            }
        }
    }
}