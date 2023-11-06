using System;
using Zenject;

namespace Utils
{
    public class StateFactory : IStateFactory
    {
        private readonly IInstantiator instantiator;

        public StateFactory(IInstantiator instantiator) => this.instantiator = instantiator;

        public T Create<T>() where T : IState => instantiator.Instantiate<T>();
    }
}