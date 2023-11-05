using System;
using Zenject;

namespace Presenter
{
    /// <summary>
    /// Презентер для счетчиков в кор игре (количество ходов, целевых блоков и тд.)
    /// </summary>
    public class CounterPresenter : ICounterPresenter
    {
        public class Factory : PlaceholderFactory<CounterPresenter> { }

        public void Enable() => throw new NotImplementedException();
        public void Disable() => throw new NotImplementedException();
        public void Destroy() => throw new NotImplementedException();
    }
}