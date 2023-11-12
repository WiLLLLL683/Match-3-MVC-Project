using CompositionRoot;
using Config;
using Model.Objects;
using Model.Services;
using System;
using System.Collections.Generic;
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
        private readonly ICounterService counterService;
        private readonly ICounterTargetConfigProvider counterConfig;
        private readonly CounterView.Factory goalViewFactory;
        private readonly CounterView.Factory restrictionViewFactory;
        private readonly Dictionary<Counter, ICounterView> counterViews = new();

        private Counter[] GoalsModel => model.CurrentLevel.goals;
        private Counter[] RestrictionsModel => model.CurrentLevel.restrictions;

        public HudPresenter(Game model,
            IHudView view,
            ICounterService counterService,
            ICounterTargetConfigProvider counterConfig,
            [Inject(Id = ViewFactoryId.Goal)] CounterView.Factory goalViewFactory,
            [Inject(Id = ViewFactoryId.Restriction)] CounterView.Factory restrictionViewFactory)
        {
            this.model = model;
            this.view = view;
            this.counterService = counterService;
            this.counterConfig = counterConfig;
            this.goalViewFactory = goalViewFactory;
            this.restrictionViewFactory = restrictionViewFactory;
        }
        public void Enable()
        {
            ClearViews();
            SpawnGoalViews();
            SpawnRestrictionViews();

            counterService.OnUpdateEvent += UpdateView;
            counterService.OnCompleteEvent += CompleteView;

            Debug.Log($"{this} enabled");
        }
        public void Disable()
        {
            counterService.OnUpdateEvent -= UpdateView;
            counterService.OnCompleteEvent -= CompleteView;

            Debug.Log($"{this} disabled");
        }

        private void ClearViews()
        {
            for (int i = 0; i < view.GoalsParent.childCount; i++)
            {
                GameObject.Destroy(view.GoalsParent.GetChild(i).gameObject);
            }

            for (int i = 0; i < view.RestrictionsParent.childCount; i++)
            {
                GameObject.Destroy(view.RestrictionsParent.GetChild(i).gameObject);
            }

            counterViews.Clear();
        }

        private void SpawnGoalViews()
        {
            for (int i = 0; i <GoalsModel.Length; i++)
            {
                ICounterView view = goalViewFactory.Create();
                InitView(view, GoalsModel[i]);
            }
        }

        private void SpawnRestrictionViews()
        {
            for (int i = 0; i < RestrictionsModel.Length; i++)
            {
                ICounterView view = restrictionViewFactory.Create();
                InitView(view, RestrictionsModel[i]);
            }
        }

        private void UpdateView(Counter model)
        {
            if (!counterViews.ContainsKey(model))
                return;

            counterViews[model].ChangeCount(model.Count);
        }

        private void CompleteView(Counter model)
        {
            if (!counterViews.ContainsKey(model))
                return;

            counterViews[model].ShowCompleteIcon();
        }

        private void InitView(ICounterView view, Counter model)
        {
            var icon = counterConfig.GetSO(model.Target.Id).icon;
            var count = model.Count;
            view.Init(icon, count);
            counterViews.Add(model, view);
        }
    }
}