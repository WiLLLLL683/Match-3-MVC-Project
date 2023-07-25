using Model.Objects;
using Model.Readonly;
using System.Collections.Generic;
using UnityEngine;
using View;

namespace Presenter
{
    public class CounterFactory : IFactory<Counter, ICounterView>
    {
        private Transform parent;
        private ICounterView counterPrefab;

        private List<ICounterPresenter> allCounters = new();

        public ICounterView Create(Counter model)
        {
            ICounterView view = GameObject.Instantiate(counterPrefab, parent);
            ICounterPresenter presenter = new CounterPresenter();
            presenter.Init();
            view.Init(model.Target.Icon, model.Count);
            allCounters.Add(presenter);
            return view;
        }
        public void Clear()
        {
            for (int i = 0; i < allCounters.Count; i++)
            {
                allCounters[i].Destroy();
            }

            allCounters.Clear();

            //уничтожить неучтенные объекты
            for (int i = 0; i < parent.childCount; i++)
            {
                GameObject.Destroy(parent.GetChild(i).gameObject);
            }
        }
    }
}