using Config;
using Model.Objects;
using Model.Services;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using View;

namespace Presenter.UnitTests
{
    public class HeaderPresenterTests
    {
        private int viewUpdatedTimes;
        private int viewInitializedTimes;

        private (ICurrencyService service, HeaderPresenter presenter) Setup()
        {
            viewUpdatedTimes = 0;
            viewInitializedTimes = 0;

            var service = Substitute.For<ICurrencyService>();

            var view = Substitute.For<IHeaderView>();
            var viewCounter = Substitute.For<ICounterView>();
            view.StarsCounter.Returns(viewCounter);
            viewCounter.When(x => x.ChangeCount(Arg.Any<int>())).Do(x => viewUpdatedTimes++);
            viewCounter.When(x => x.Init(Arg.Any<Sprite>(), Arg.Any<int>())).Do(x => viewInitializedTimes++);

            var currencyConfig = Substitute.For<ICurrencyConfigProvider>();
            var SO = Substitute.For<CurrencySO>();
            currencyConfig.GetSO(Arg.Any<CurrencyType>()).Returns(SO);

            var presenter = new HeaderPresenter(service, view, currencyConfig);

            return (service, presenter);
        }

        [Test]
        public void Enable_ViewInitialized()
        {
            var (service, presenter) = Setup();

            presenter.Enable();

            Assert.AreEqual(1, viewInitializedTimes);
        }

        [Test]
        public void OnChangeEvent_ViewUpdated()
        {
            var (service, presenter) = Setup();
            presenter.Enable();

            service.OnChange += Raise.Event<Action<CurrencyType, int>>(CurrencyType.Star, 10);

            Assert.AreEqual(1, viewUpdatedTimes);
        }

        [Test]
        public void Disable_OnChangeEvent_ViewNotUpdated()
        {
            var (service, presenter) = Setup();
            presenter.Enable();
            presenter.Disable();

            service.OnChange += Raise.Event<Action<CurrencyType, int>>(CurrencyType.Star, 10);

            Assert.AreEqual(0, viewUpdatedTimes);
        }
    }
}