using Model.Readonly;
using Presenter;
using System;
using System.Collections;
using UnityEngine;
using View;

public class LevelSelectionScreen : ALevelSelectionScreen
{
    [SerializeField] private ASelectorView selector;

    private ISelectorPresenter presenter;
    private ILevelSelection_Readonly model;
    private AFactory<ILevelSelection_Readonly, ASelectorView, ISelectorPresenter> selectorFactory;

    public override void Init(ILevelSelection_Readonly model,
                     AFactory<ILevelSelection_Readonly, ASelectorView, ISelectorPresenter> selectorFactory)
    {
        this.model = model;
        this.selectorFactory = selectorFactory;
    }
    public override void Enable()
    {
        presenter = selectorFactory.Connect(selector, model);
        Debug.Log($"{this} enabled", this);
    }
    public override void Disable()
    {
        presenter.Disable();
        Debug.Log($"{this} disabled", this);
    }
}
