using Config;
using Model.Objects;
using Model.Services;
using System;
using UnityEngine;
using View;

namespace Presenter
{
    /// <summary>
    /// Презентер для верхней панели в мета игре
    /// Отображает количество очков из модели
    /// </summary>
    public class HeaderPresenter : IHeaderPresenter
    {
        private readonly ICurrencyService model;
        private readonly IHeaderView view;
        private readonly CurrencySetSO allCurrencies;

        public HeaderPresenter(ICurrencyService model,
            IHeaderView view,
            CurrencySetSO allCurrencies)
        {
            this.model = model;
            this.view = view;
            this.allCurrencies = allCurrencies;
        }
        public void Enable()
        {
            model.OnChange += UpdateView;

            InitView();
            Debug.Log($"{this} enabled");
        }

        public void Disable()
        {
            model.OnChange -= UpdateView;

            Debug.Log($"{this} disabled");
        }

        private void InitView()
        {
            var starsSO = allCurrencies.GetSO(CurrencyType.Star);
            view.StarsCounter.Init(starsSO.icon, model.GetAmount(CurrencyType.Star));
        }

        private void UpdateView(CurrencyType type, int amount)
        {
            if (type != CurrencyType.Star)
                return;

            view.StarsCounter.ChangeCount(amount);
        }
    }
}