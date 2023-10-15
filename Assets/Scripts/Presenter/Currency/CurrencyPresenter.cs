using System;
using UnityEngine;
using Model.Objects;
using Utils;
using View;
using Model.Readonly;

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
        public class Factory : AFactory<ICurrencyService_Readonly, ACounterView, ICurrencyPresenter>
        {
            public Factory(ACounterView viewPrefab, Transform parent = null) : base(viewPrefab)
            {
            }

            public override ICurrencyPresenter Connect(ACounterView existingView, ICurrencyService_Readonly model)
            {
                throw new NotImplementedException();
            }
        }
        
        public void Destroy() => throw new NotImplementedException();
        public void Disable() => throw new NotImplementedException();
        public void Enable() => throw new NotImplementedException();
    }
}