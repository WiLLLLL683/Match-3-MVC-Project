using System;
using System.Collections.Generic;
using UnityEngine;
using Model.Objects;
using View;

public class BoosterPresenter : IBoosterPresenter
{
    private IBoosterView view;
    private IBooster model;

    public BoosterPresenter(IBoosterView view, IBooster model)
    {
        this.view = view;
        this.model = model;
    }
    public void Init()
    {

    }
    public void Destroy()
    {

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
