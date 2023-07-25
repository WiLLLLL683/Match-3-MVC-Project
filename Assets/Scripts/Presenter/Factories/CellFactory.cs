using Model.Readonly;
using View;
using System.Collections.Generic;
using UnityEngine;
using AYellowpaper;

namespace Presenter
{
    public class CellFactory : IFactory<ICell_Readonly, ICellView>
    {
        private Transform parent;
        private ICellView cellPrefab;

        private List<ICellPresenter> allCells = new();

        public CellFactory(ICellView viewPrefab, Transform parent)
        {
            this.cellPrefab = viewPrefab;
            this.parent = parent;
        }
        public ICellView Create(ICell_Readonly cellModel)
        {
            ICellView cellView = GameObject.Instantiate(cellPrefab, parent);
            ICellPresenter cellPresenter = new CellPresenter(cellModel, cellView);
            cellPresenter.Init();
            allCells.Add(cellPresenter);
            return cellView;
        }
        public void Clear()
        {
            for (int i = 0; i < allCells.Count; i++)
            {
                allCells[i].Destroy(null);
            }

            //уничтожить неучтенные объекты
            for (int i = 0; i < parent.childCount; i++)
            {
                GameObject.Destroy(parent.GetChild(i).gameObject);
            }
        }
    }
}