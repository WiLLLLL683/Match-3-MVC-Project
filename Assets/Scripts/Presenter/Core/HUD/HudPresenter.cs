using System;
using System.Collections.Generic;
using Model.Objects;
using Model.Services;
using UnityEngine;
using View;
using View.Factories;

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
        private readonly ICounterViewFactory counterViewFactory;
        private readonly Dictionary<Counter, ICounterView> counterViews = new();

        private Counter[] GoalsModel => model.CurrentLevel.goals;
        private Counter[] RestrictionsModel => model.CurrentLevel.restrictions;

        public HudPresenter(Game model,
            IHudView view,
            ICounterService counterService,
            ICounterViewFactory counterViewFactory)
        {
            this.model = model;
            this.view = view;
            this.counterService = counterService;
            this.counterViewFactory = counterViewFactory;
        }
        public void Enable()
        {
            ClearViews();
            SpawnGoalViews();
            SpawnRestrictionViews();

            counterService.OnUpdateEvent += UpdateView;
            counterService.OnCompleteEvent += CompleteView;

            //Debug.Log($"{this} enabled");
        }

        public void Disable()
        {
            counterService.OnUpdateEvent -= UpdateView;
            counterService.OnCompleteEvent -= CompleteView;

            //Debug.Log($"{this} disabled");
        }

        public bool TryGetCounterView(Counter model, out ICounterView view)
        {
            if (counterViews.ContainsKey(model))
            {
                view = counterViews[model];
                return true;
            }

            view = null;
            return false;
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
                ICounterView counterView = counterViewFactory.CreateGoal(GoalsModel[i]);
                counterViews.Add(GoalsModel[i], counterView);
            }
        }

        private void SpawnRestrictionViews()
        {
            for (int i = 0; i < RestrictionsModel.Length; i++)
            {
                ICounterView counterView = counterViewFactory.CreateRestriction(RestrictionsModel[i]);
                counterViews.Add(RestrictionsModel[i], counterView);
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
    }
}