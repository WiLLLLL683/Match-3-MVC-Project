using UnityEngine;
using Model.Readonly;
using Presenter;
using View;
using Utils;
using Model.Objects;

public class LevelSelectionPresenter : ILevelSelectionPresenter
{
    public class Factory : AFactory<LevelProgress, ALevelSelectionView, ILevelSelectionPresenter>
    {
        private readonly AFactory<LevelProgress, ASelectorView, ISelectorPresenter> selectorFactory;
        public Factory(ALevelSelectionView viewPrefab, AFactory<LevelProgress, ASelectorView, ISelectorPresenter> selectorFactory) : base(viewPrefab)
        {
            this.selectorFactory = selectorFactory;
        }

        public override ILevelSelectionPresenter Connect(ALevelSelectionView existingView, LevelProgress model)
        {
            var presenter = new LevelSelectionPresenter(model, existingView, selectorFactory);
            presenter.Enable();
            allPresenters.Add(presenter);
            return presenter;
        }
    }

    private LevelProgress model;
    private ALevelSelectionView view;
    private AFactory<LevelProgress, ASelectorView, ISelectorPresenter> selectorFactory;
    
    private ISelectorPresenter selector;

    public LevelSelectionPresenter(LevelProgress model,
                     ALevelSelectionView view,
                     AFactory<LevelProgress, ASelectorView, ISelectorPresenter> selectorFactory)
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
