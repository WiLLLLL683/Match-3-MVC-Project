using System;
using Zenject;

namespace Presenter
{
    /// <summary>
    /// Презентер для счетчика валюты "звезды" в мета игре
    /// </summary>
    public class CurrencyPresenter : ICurrencyPresenter
    {
        public class Factory : PlaceholderFactory<CurrencyPresenter> { }

        public void Destroy() => throw new NotImplementedException();
        public void Disable() => throw new NotImplementedException();
        public void Enable() => throw new NotImplementedException();
    }
}