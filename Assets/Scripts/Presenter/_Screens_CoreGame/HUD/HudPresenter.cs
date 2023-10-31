using UnityEngine;
using View;
using Utils;
using Model.Objects;
using Zenject;
using System;

namespace Presenter
{
    /// <summary>
    /// Контроллер для HUD
    /// </summary>
    public class HudPresenter : IHudPresenter, IInitializable, IDisposable
    {
        //public class Factory : AFactory<Level, AHudView, IHudPresenter>
        //{
        //    private AFactory<Counter, ACounterView, ICounterPresenter> goalFactory;
        //    private AFactory<Counter, ACounterView, ICounterPresenter> restrictionFactory;
        //    public Factory(AHudView viewPrefab,
        //        AFactory<Counter, ACounterView, ICounterPresenter> goalFactory,
        //        AFactory<Counter, ACounterView, ICounterPresenter> restrictionFactory) : base(viewPrefab)
        //    {
        //        this.goalFactory = goalFactory;
        //        this.restrictionFactory = restrictionFactory;
        //    }

        //    public override IHudPresenter Connect(AHudView existingView, Level model)
        //    {
        //        var presenter = new HudPresenter(model, existingView, goalFactory, restrictionFactory);
        //        presenter.Enable();
        //        allPresenters.Add(presenter);
        //        return presenter;
        //    }
        //}
        
        private readonly Level model;
        private readonly AHudView view;
        private readonly CounterPresenter.Factory counterPresenterFactory;
        private readonly CounterView.Factory goalViewFactory;
        private readonly CounterView.Factory restrictionViewFactory;

        public HudPresenter(Level model,
            AHudView view,
            CounterPresenter.Factory counterPresenterFactory,
            [Inject(Id = "goalViewFactory")] CounterView.Factory goalViewFactory,
            [Inject(Id = "restrictionViewFactory")] CounterView.Factory restrictionViewFactory)
        {
            this.model = model;
            this.view = view;
            this.counterPresenterFactory = counterPresenterFactory;
            this.goalViewFactory = goalViewFactory;
            this.restrictionViewFactory = restrictionViewFactory;
        }
        public void Initialize()
        {
            //TODO
            Debug.Log($"{this} enabled");
        }
        public void Dispose()
        {
            //TODO
            Debug.Log($"{this} disabled");
        }
    }
}