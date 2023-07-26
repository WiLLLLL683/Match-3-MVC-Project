using Model.Readonly;
using View;
using System.Collections.Generic;
using UnityEngine;
using AYellowpaper;

namespace Presenter
{
    public class CellFactory : AFactory<ICell_Readonly, ICellView, ICellPresenter>
    {
        public CellFactory(ICellView viewPrefab, Transform parent = null) : base(viewPrefab, parent)
        {
        }

        public override ICellView Create(ICell_Readonly model, out ICellPresenter presenter)
        {
            ICellView view = GameObject.Instantiate(viewPrefab, parent);
            presenter = new CellPresenter(model, view);
            view.Init(model.Position, model.Type);
            allPresenters.Add(presenter);
            presenter.Enable();
            return view;
        }
    }
}