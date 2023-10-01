using Model.Objects;
using System;

namespace Model.Factories
{
    public interface ICounterFactory
    {
        public Counter[] Create(CounterSO[] configs);
        public Counter Create(CounterSO config);
    }
}