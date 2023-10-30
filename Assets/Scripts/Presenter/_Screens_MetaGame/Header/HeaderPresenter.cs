using Model.Objects;
using UnityEngine;
using View;
using Utils;
using Zenject;
using Model.Services;
using System;

namespace Presenter
{
    /// <summary>
    /// Верхняя панель с валютами в мета игре
    /// </summary>
    public class HeaderPresenter : IHeaderPresenter, IInitializable, IDisposable
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
        public void Initialize()
        {
            Debug.Log($"{this} enabled");
        }
        public void Dispose()
        {
            Debug.Log($"{this} disabled");
        }
    }
}