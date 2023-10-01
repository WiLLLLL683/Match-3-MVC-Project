using Model.Objects;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Model.Factories
{
    public class CounterFactory : ICounterFactory
    {
        public Counter Create(CounterSO config) => new(config.target, config.count);

        public Counter[] Create(CounterSO[] configs)
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