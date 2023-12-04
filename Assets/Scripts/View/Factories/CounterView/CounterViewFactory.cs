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
        private readonly CounterView goalPrefab;
        private readonly CounterView restrictionPrefab;

        public CounterViewFactory(IInstantiator instantiator,
            IConfigProvider configProvider,
            IHudView hudView,
            CounterView goalPrefab,
            CounterView restrictionPrefab)
        {
            this.instantiator = instantiator;
            this.configProvider = configProvider;
            this.hudView = hudView;
            this.goalPrefab = goalPrefab;
            this.restrictionPrefab = restrictionPrefab;
        }

        public ICounterView CreateGoal(Counter model)
        {
            CounterView view = instantiator.InstantiatePrefabForComponent<CounterView>(goalPrefab, hudView.GoalsParent);
            InitView(view, model);
            return view;
        }

        public ICounterView CreateRestriction(Counter model)
        {
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