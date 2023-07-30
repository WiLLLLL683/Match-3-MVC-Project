using Model.Objects;
using System;
using System.Collections.Generic;
using UnityEngine;
using View;

namespace Presenter
{
    public class CurrencyFactory : AFactory<CurrencyInventory, ICounterView, ICurrencyPresenter>
    {
        public CurrencyFactory(ICounterView viewPrefab, Transform parent = null) : base(viewPrefab, parent)
        {
        }

        public override ICurrencyPresenter Connect(ICounterView existingView, CurrencyInventory model)
        {
            throw new NotImplementedException();
        }

        public override ICounterView CreateView(CurrencyInventory model, out ICurrencyPresenter presenter)
        {
            throw new NotImplementedException();
        }
    }
}