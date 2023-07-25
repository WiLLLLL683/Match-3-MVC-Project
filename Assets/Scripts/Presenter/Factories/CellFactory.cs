using Model.Readonly;
using View;
using System.Collections.Generic;
using UnityEngine;
using AYellowpaper;

namespace Presenter
{
    public class CellFactory : FactoryBase<ICell_Readonly, ICellView>
    {
        private List<ICellPresenter> allCells = new();

        public CellFactory(ICellView viewPrefab, Transform parent = null) : base(viewPrefab, parent)
        {
        }

        public override ICellView Create(ICell_Readonly model)
        {
            ICellView view = GameObject.Instantiate(viewPrefab, parent);
            ICellPresenter presenter = new CellPresenter(model, view);
            view.Init(model.Position, model.Type);
            allCells.Add(presenter);
            presenter.Enable();
            return view;
        }
        public override void Clear()
        {
            for (int i = 0; i < allCells.Count; i++)
            {
                allCells[i].Destroy(null);
            }
            
            allCells.Clear();
        }
    }
}