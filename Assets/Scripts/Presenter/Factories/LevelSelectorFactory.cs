using Model.Readonly;
using Presenter;
using System;
using System.Collections.Generic;
using UnityEngine;
using View;

public class LevelSelectorFactory : AFactory<ILevelSelection_Readonly, ASelectorView, ISelectorPresenter>
{
    private readonly Bootstrap bootstrap;
    public LevelSelectorFactory(ASelectorView viewPrefab, Bootstrap bootstrap, Transform parent = null) : base(viewPrefab, parent)
    {
        this.bootstrap = bootstrap;
    }

    public override ISelectorPresenter Connect(ASelectorView existingView, ILevelSelection_Readonly model)
    {
        var presenter = new LevelSelectorPresenter(model, existingView, bootstrap);
        presenter.Enable();
        existingView.Init(model.CurrentLevelData.Icon, model.CurrentLevelData.LevelName);
        allPresenters.Add(presenter);
        return presenter;
    }

    public override ASelectorView CreateView(ILevelSelection_Readonly model, out ISelectorPresenter presenter)
    {
        var view = GameObject.Instantiate(viewPrefab, parent);
        presenter = Connect(view, model);
        return view;
    }
}
