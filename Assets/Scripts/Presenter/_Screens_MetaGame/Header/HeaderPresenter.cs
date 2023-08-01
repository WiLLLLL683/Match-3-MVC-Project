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
    public class HeaderPresenter : IHeaderPresenter
    {
        public class Factory : AFactory<CurrencyInventory, AHeaderView, IHeaderPresenter>
        {
            private readonly AFactory<CurrencyInventory, ACounterView, ICurrencyPresenter> currencyFactory;
            public Factory(AHeaderView viewPrefab, AFactory<CurrencyInventory, ACounterView, ICurrencyPresenter> currencyFactory) : base(viewPrefab)
            {
                this.currencyFactory = currencyFactory;
            }

            public override IHeaderPresenter Connect(AHeaderView existingView, CurrencyInventory model)
            {
                var presenter = new HeaderPresenter(model, existingView, currencyFactory);
                presenter.Enable();
                allPresenters.Add(presenter);
                return presenter;
            }
        }

        private CurrencyInventory model;
        private AHeaderView view;
        private AFactory<CurrencyInventory, ACounterView, ICurrencyPresenter> scoreFactory;

        public HeaderPresenter(CurrencyInventory model, AHeaderView view, AFactory<CurrencyInventory, ACounterView, ICurrencyPresenter> scoreFactory)
        {
            this.model = model;
            this.view = view;
            this.scoreFactory = scoreFactory;
            this.scoreFactory.SetParent(view.ScoreParent);
        }
        public void Enable()
        {
            Debug.Log($"{this} enabled");
        }
        public void Disable()
        {
            Debug.Log($"{this} disabled");
        }
        public void Destroy()
        {
            Disable();
            GameObject.Destroy(view.gameObject);
        }
    }
}