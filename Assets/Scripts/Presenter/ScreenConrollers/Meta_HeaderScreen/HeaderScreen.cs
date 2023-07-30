using Model.Objects;
using Model.Readonly;
using Presenter;
using System;
using System.Collections;
using UnityEngine;
using View;
using Utils;

namespace Presenter
{
    /// <summary>
    /// Верхняя панель с валютами в мета игре
    /// </summary>
    public class HeaderScreen : AHeaderScreen
    {
        [SerializeField] private Transform currencyParent;

        private CurrencyInventory model;
        private AFactory<CurrencyInventory, ICounterView, ICurrencyPresenter> currencyFactory;

        public override void Init(CurrencyInventory model, AFactory<CurrencyInventory, ICounterView, ICurrencyPresenter> scoreFactory)
        {
            this.model = model;
            this.currencyFactory = scoreFactory;
            this.currencyFactory.SetParent(currencyParent);
        }
        public override void Enable()
        {
            Debug.Log($"{this} enabled", this);
        }
        public override void Disable()
        {
            Debug.Log($"{this} disabled", this);
        }
    }
}