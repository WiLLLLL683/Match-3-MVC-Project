using Config;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace View.Factories
{
    public class BoosterButtonFactory : IBoosterButtonFactory
    {
        private readonly IInstantiator instantiator;
        private readonly IBoostersView boostersView;
        private readonly IConfigProvider configProvider;

        public BoosterButtonFactory(IInstantiator instantiator, IBoostersView boostersView, IConfigProvider configProvider)
        {
            this.instantiator = instantiator;
            this.boostersView = boostersView;
            this.configProvider = configProvider;
        }

        public IBoosterButtonView Create(int id, int amount)
        {
            BoosterSO config = configProvider.GetBoosterSO(id);
            IBoosterButtonView view = instantiator.InstantiatePrefabForComponent<BoosterButtonView>(configProvider.Prefabs.boosterButtonPrefab, boostersView.BoosterButtonsParent);
            bool isEnabled = amount > 0;
            view.Init(id, config.Icon, amount, isEnabled);
            return view;
        }
    }
}