using Model.Objects;
using System;
using System.Collections.Generic;
using UnityEngine;
using View;
using Utils;

namespace Presenter
{
    public abstract class AHeaderScreen : AScreenController
    {
        public abstract void Init(CurrencyInventory model, AFactory<CurrencyInventory, ICounterView, ICurrencyPresenter> scoreFactory);

        /// <summary>
        /// Factory-method
        /// Находится здесь, так как нет необходимости передавать объект фабрики как зависимость.
        /// Инпут создается только из Bootstrap.
        /// </summary>
        public static AHeaderScreen Create(AHeaderScreen prefab, CurrencyInventory currencyInventory, AFactory<CurrencyInventory, ICounterView, ICurrencyPresenter> scoreFactory)
        {
            var headerScreen = GameObject.Instantiate(prefab);
            headerScreen.Init(currencyInventory, scoreFactory);
            headerScreen.Enable();
            return headerScreen;
        }
    }
}