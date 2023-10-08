using Config;
using Model.Objects;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Model.Factories
{
    public class CounterFactory : ICounterFactory
    {
        public Counter Create(LevelSO.CounterConfig config)
        {
            if (config == null)
                return null;
            if (config.target?.CounterTarget == null)
                return null;

            return new(config.target.CounterTarget, config.count);
        }

        public Counter[] Create(LevelSO.CounterConfig[] configs)
        {
            Counter[] counters = new Counter[configs.Length];

            for (int i = 0; i < counters.Length; i++)
            {
                counters[i] = Create(configs[i]);
            }

            return counters;
        }
    }
}