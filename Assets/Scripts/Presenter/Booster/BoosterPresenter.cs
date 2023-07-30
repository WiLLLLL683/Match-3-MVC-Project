using System;
using System.Collections.Generic;
using UnityEngine;
using Model.Objects;
using View;
using Utils;

public class BoosterPresenter : IBoosterPresenter
{
    /// <summary>
    /// Реализация фабрики использующая класс презентера в котором находится.
    /// </summary>
    public class Factory : AFactory<IBooster, IBoosterView, IBoosterPresenter>
    {
        public Factory(IBoosterView viewPrefab, Transform parent = null) : base(viewPrefab, parent)
        {
        }

        public override IBoosterPresenter Connect(IBoosterView existingView, IBooster model)
        {
            var presenter = new BoosterPresenter(existingView, model);
            existingView.Init(model.Icon, model.Amount);
            allPresenters.Add(presenter);
            presenter.Enable();
            return presenter;
        }
    }
    
    private IBoosterView view;
    private IBooster model;

    public BoosterPresenter(IBoosterView view, IBooster model)
    {
        this.view = view;
        this.model = model;
    }
    public void Enable()
    {

    }
    public void Disable()
    {

    }
    public void Destroy()
    {
        Disable();
        GameObject.Destroy(view.gameObject);
    }



    private void ChangeAmount(int amount)
    {
        if (amount > 0)
            view.EnableButton();
        else
            view.DisableButton();

        view.ChangeAmount(amount);
    }
    private void ChangeIcon(Sprite icon) => view.ChangeIcon(icon);
    private void ActivateBooster()
    {
        //TODO call model
    }
}
