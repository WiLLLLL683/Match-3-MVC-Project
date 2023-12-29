using Config;
using System;
using System.Collections.Generic;
using UnityEngine;
using Model.Objects;
using Zenject;

namespace View.Factories
{
    /// <summary>
    /// Фабрика элементов вью - счетчиков, предназначена специально для HUD
    /// </summary>
    public class CounterViewFactory : ICounterViewFactory
    {
        private readonly IInstantiator instantiator;
        private readonly IConfigProvider configProvider;
        private readonly IHudView hudView;

        public CounterViewFactory(IInstantiator instantiator,
            IConfigProvider configProvider,
            IHudView hudView)
        {
            this.instantiator = instantiator;
            this.configProvider = configProvider;
            this.hudView = hudView;
        }

        public ICounterView CreateGoal(Counter model)
        {
            var goalPrefab = configProvider.Prefabs.goalCounterPrefab;
            CounterView view = instantiator.InstantiatePrefabForComponent<CounterView>(goalPrefab, hudView.GoalsParent);
            InitView(view, model);
            return view;
        }

        public ICounterView CreateRestriction(Counter model)
        {
            var restrictionPrefab = configProvider.Prefabs.restrictionCounterPrefab;
            CounterView view = instantiator.InstantiatePrefabForComponent<CounterView>(restrictionPrefab, hudView.RestrictionsParent);
            InitView(view, model);
            return view;
        }

        private void InitView(ICounterView view, Counter model)
        {
            Sprite icon = configProvider.GetCounterTargetSO(model.Target.Id).icon;
            int count = model.Count;
            view.Init(icon, count);
        }
    }
}