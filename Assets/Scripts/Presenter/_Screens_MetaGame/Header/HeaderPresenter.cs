using Model.Objects;
using UnityEngine;
using View;
using Utils;
using Model.Readonly;

namespace Presenter
{
    /// <summary>
    /// Верхняя панель с валютами в мета игре
    /// </summary>
    public class HeaderPresenter : IHeaderPresenter
    {
        public class Factory : AFactory<ICurrencyService_Readonly, AHeaderView, IHeaderPresenter>
        {
            private readonly AFactory<ICurrencyService_Readonly, ACounterView, ICurrencyPresenter> currencyFactory;
            public Factory(AHeaderView viewPrefab, AFactory<ICurrencyService_Readonly, ACounterView, ICurrencyPresenter> currencyFactory) : base(viewPrefab)
            {
                this.currencyFactory = currencyFactory;
            }

            public override IHeaderPresenter Connect(AHeaderView existingView, ICurrencyService_Readonly model)
            {
                var presenter = new HeaderPresenter(model, existingView, currencyFactory);
                presenter.Enable();
                allPresenters.Add(presenter);
                return presenter;
            }
        }

        private ICurrencyService_Readonly model;
        private AHeaderView view;
        private AFactory<ICurrencyService_Readonly, ACounterView, ICurrencyPresenter> scoreFactory;

        public HeaderPresenter(ICurrencyService_Readonly model, AHeaderView view, AFactory<ICurrencyService_Readonly, ACounterView, ICurrencyPresenter> scoreFactory)
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