using Model.Readonly;
using Presenter;
using System;
using System.Collections.Generic;
using UnityEngine;
using View;

namespace Presenter
{
    public class HudFactory : AFactory<ILevel_Readonly, AHudView, IHudPresenter>
    {
        private readonly AFactory<ICounter_Readonly, ICounterView, ICounterPresenter> goalFactory;
        private readonly AFactory<ICounter_Readonly, ICounterView, ICounterPresenter> restrictionFactory;

        public HudFactory(AHudView viewPrefab,
            AFactory<ICounter_Readonly, ICounterView, ICounterPresenter> goalFactory,
            AFactory<ICounter_Readonly, ICounterView, ICounterPresenter> restrictionFactory,
            Transform parent = null) : base(viewPrefab, parent)
        {
            this.goalFactory = goalFactory;
            this.restrictionFactory = restrictionFactory;
        }

        public override AHudView Create(ILevel_Readonly model, out IHudPresenter presenter)
        {
            var view = GameObject.Instantiate(viewPrefab, parent);
            presenter = new HudPresenter(model, view, goalFactory, restrictionFactory);
            presenter.Enable();
            allPresenters.Add(presenter);
            return view;
        }
    }
}