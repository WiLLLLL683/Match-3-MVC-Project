using Config;
using Model.Objects;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Model.Factories
{
    public class BoosterFactory : IBoosterFactory
    {
        private readonly IConfigProvider configProvider;

        public BoosterFactory(IConfigProvider configProvider)
        {
            this.configProvider = configProvider;
        }

        public IBooster Create(int id)
        {
            return configProvider.GetBoosterSO(id).booster.Clone();
        }
    }
}