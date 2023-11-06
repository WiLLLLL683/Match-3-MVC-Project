using Model.Objects;
using System;
using UnityEngine;
using View;
using Zenject;

namespace Presenter
{
    /// <summary>
    /// Презентер для HUD
    /// Отображает данные модели о счетчиках целей и ограничений
    /// </summary>
    public class HudPresenter : IHudPresenter
    {
        private readonly Game model;
        private readonly IHudView view;
        private readonly CounterPresenter.Factory counterPresenterFactory;
        private readonly CounterView.Factory goalViewFactory;
        private readonly CounterView.Factory restrictionViewFactory;

        public HudPresenter(Game model,
            IHudView view,
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
        public void Enable()
        {
            //TODO
            Debug.Log($"{this} enabled");
        }
        public void Disable()
        {
            //TODO
            Debug.Log($"{this} disabled");
        }
    }
}