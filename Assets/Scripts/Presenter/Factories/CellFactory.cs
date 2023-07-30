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

        public override ICellPresenter Connect(ICellView existingView, ICell_Readonly model)
        {
            var presenter = new CellPresenter(model, existingView);
            existingView.Init(model.Position, model.Type);
            allPresenters.Add(presenter);
            presenter.Enable();
            return presenter;
        }

        public override ICellView CreateView(ICell_Readonly model, out ICellPresenter presenter)
        {
            var view = GameObject.Instantiate(viewPrefab, parent);
            presenter = Connect(view, model);
            return view;
        }
    }
}