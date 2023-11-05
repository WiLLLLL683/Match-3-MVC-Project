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
        private readonly AHeaderView view;
        private readonly CurrencyPresenter.Factory scoreFactory;
        private readonly CounterView.Factory scoreViewFactory;

        public HeaderPresenter(ICurrencyService model, AHeaderView view, CurrencyPresenter.Factory scorePresenterFactory, CounterView.Factory scoreViewFactory)
        {
            this.model = model;
            this.view = view;
            this.scoreFactory = scorePresenterFactory;
            this.scoreViewFactory = scoreViewFactory;
        }
        public void Enable()
        {
            Debug.Log($"{this} enabled");
        }
        public void Disable()
        {
            Debug.Log($"{this} disabled");
        }
    }
}