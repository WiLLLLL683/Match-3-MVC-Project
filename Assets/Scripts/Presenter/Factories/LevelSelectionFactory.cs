using Model.Readonly;
using Presenter;
using System;
using System.Collections.Generic;
using UnityEngine;
using View;

namespace Presenter
{
    public class LevelSelectionFactory : AFactory<ILevelSelection_Readonly, ALevelSelectionView, ILevelSelectionPresenter>
    {
        private readonly Bootstrap bootstrap;
        public LevelSelectionFactory(ALevelSelectionView viewPrefab, Bootstrap bootstrap, Transform parent = null) : base(viewPrefab, parent)
        {
            this.bootstrap = bootstrap;
        }

        public override ALevelSelectionView Create(ILevelSelection_Readonly model, out ILevelSelectionPresenter presenter)
        {
            var view = GameObject.Instantiate(viewPrefab, parent);
            presenter = new LevelSelectionPresenter(model, view, bootstrap);
            presenter.Enable();
            allPresenters.Add(presenter);
            return view;
        }
    }
}