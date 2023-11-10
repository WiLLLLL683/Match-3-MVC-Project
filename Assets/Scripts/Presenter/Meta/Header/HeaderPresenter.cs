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
        private readonly ICurrencyService currencyService;
        private readonly IHeaderView view;
        private readonly ICurrencyConfigProvider currencyConfig;

        public HeaderPresenter(ICurrencyService currencyService,
            IHeaderView view,
            ICurrencyConfigProvider currencyConfig)
        {
            this.currencyService = currencyService;
            this.view = view;
            this.currencyConfig = currencyConfig;
        }
        public void Enable()
        {
            currencyService.OnChange += UpdateView;

            InitView();
            Debug.Log($"{this} enabled");
        }

        public void Disable()
        {
            currencyService.OnChange -= UpdateView;

            Debug.Log($"{this} disabled");
        }

        private void InitView()
        {
            var starsSO = currencyConfig.GetSO(CurrencyType.Star);
            view.StarsCounter.Init(starsSO.icon, currencyService.GetAmount(CurrencyType.Star));
        }

        private void UpdateView(CurrencyType type, int amount)
        {
            if (type != CurrencyType.Star)
                return;

            view.StarsCounter.ChangeCount(amount);
        }
    }
}