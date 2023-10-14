using UnityEngine;
using Model.Readonly;
using Presenter;
using View;
using Utils;

public class LevelSelectionPresenter : ILevelSelectionPresenter
{
    public class Factory : AFactory<ILevelProgress_Readonly, ALevelSelectionView, ILevelSelectionPresenter>
    {
        private readonly AFactory<ILevelProgress_Readonly, ASelectorView, ISelectorPresenter> selectorFactory;
        public Factory(ALevelSelectionView viewPrefab, AFactory<ILevelProgress_Readonly, ASelectorView, ISelectorPresenter> selectorFactory) : base(viewPrefab)
        {
            this.selectorFactory = selectorFactory;
        }

        public override ILevelSelectionPresenter Connect(ALevelSelectionView existingView, ILevelProgress_Readonly model)
        {
            var presenter = new LevelSelectionPresenter(model, existingView, selectorFactory);
            presenter.Enable();
            allPresenters.Add(presenter);
            return presenter;
        }
    }

    private ILevelProgress_Readonly model;
    private ALevelSelectionView view;
    private AFactory<ILevelProgress_Readonly, ASelectorView, ISelectorPresenter> selectorFactory;
    
    private ISelectorPresenter selector;

    public LevelSelectionPresenter(ILevelProgress_Readonly model,
                     ALevelSelectionView view,
                     AFactory<ILevelProgress_Readonly, ASelectorView, ISelectorPresenter> selectorFactory)
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
