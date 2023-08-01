using Model.Readonly;
using Presenter;
using System;
using System.Collections;
using UnityEngine;
using View;
using Utils;

public class LevelSelectionPresenter : ILevelSelectionPresenter
{
    public class Factory : AFactory<ILevelSelection_Readonly, ALevelSelectionView, ILevelSelectionPresenter>
    {
        private readonly AFactory<ILevelSelection_Readonly, ASelectorView, ISelectorPresenter> selectorFactory;
        public Factory(ALevelSelectionView viewPrefab, AFactory<ILevelSelection_Readonly, ASelectorView, ISelectorPresenter> selectorFactory) : base(viewPrefab)
        {
            this.selectorFactory = selectorFactory;
        }

        public override ILevelSelectionPresenter Connect(ALevelSelectionView existingView, ILevelSelection_Readonly model)
        {
            var presenter = new LevelSelectionPresenter(model, existingView, selectorFactory);
            presenter.Enable();
            allPresenters.Add(presenter);
            return presenter;
        }
    }

    private ILevelSelection_Readonly model;
    private ALevelSelectionView view;
    private AFactory<ILevelSelection_Readonly, ASelectorView, ISelectorPresenter> selectorFactory;
    
    private ISelectorPresenter selector;

    public LevelSelectionPresenter(ILevelSelection_Readonly model,
                     ALevelSelectionView view,
                     AFactory<ILevelSelection_Readonly, ASelectorView, ISelectorPresenter> selectorFactory)
    {
        this.model = model;
        this.view = view;
        this.selectorFactory = selectorFactory;
    }
    public void Enable()
    {
        selector = selectorFactory.Connect(view.Selector, model);
        Debug.Log($"{this} enabled");
    }
    public void Disable()
    {
        selector.Disable();
        Debug.Log($"{this} disabled");
    }
    public void Destroy()
    {
        Disable();
        GameObject.Destroy(view.gameObject);
    }
}
