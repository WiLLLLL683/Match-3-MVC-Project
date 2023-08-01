using Model.Objects;
using System;
using System.Collections.Generic;
using UnityEngine;
using Utils;
using View;

namespace Presenter
{
    /// <summary>
    /// Презентер для счетчика валюты "звезды" в мета игре
    /// </summary>
    public class CurrencyPresenter : ICurrencyPresenter
    {
        /// <summary>
        /// Реализация фабрики использующая класс презентера в котором находится.
        /// </summary>
        public class Factory : AFactory<CurrencyInventory, ICounterView, ICurrencyPresenter>
        {
            public Factory(ICounterView viewPrefab, Transform parent = null) : base(viewPrefab)
            {
            }

            public override ICurrencyPresenter Connect(ICounterView existingView, CurrencyInventory model)
            {
                throw new NotImplementedException();
            }
        }
        
        public void Destroy() => throw new NotImplementedException();
        public void Disable() => throw new NotImplementedException();
        public void Enable() => throw new NotImplementedException();
    }
}