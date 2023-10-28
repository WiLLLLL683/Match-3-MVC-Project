using Config;
using Model.Objects;
using System;

namespace Model.Factories
{
    public interface ICounterFactory
    {
        public Counter[] Create(LevelSO.CounterConfig[] configs);
        public Counter Create(LevelSO.CounterConfig config);
    }
}