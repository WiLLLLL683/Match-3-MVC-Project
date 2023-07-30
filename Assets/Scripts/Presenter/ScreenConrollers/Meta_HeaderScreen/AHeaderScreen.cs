using Model.Objects;
using System;
using System.Collections.Generic;
using UnityEngine;
using View;

namespace Presenter
{
    public abstract class AHeaderScreen : AScreenController
    {
        public abstract void Init(CurrencyInventory model, AFactory<CurrencyInventory, ICounterView, ICurrencyPresenter> scoreFactory);
    }
}