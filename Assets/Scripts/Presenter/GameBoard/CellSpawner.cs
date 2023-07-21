using Model.Readonly;
using View;
using System.Collections.Generic;
using UnityEngine;
using AYellowpaper;

namespace Presenter
{
    public class CellSpawner : MonoBehaviour
    {
        [SerializeField] private Transform parent;
        [SerializeField] private InterfaceReference<ICellView, MonoBehaviour> cellPrefab;

        private List<ICellPresenter> allCells = new();

        public ICellView SpawnCell(ICell_Readonly cellModel)
        {
            ICellView cellView = (ICellView)Instantiate(cellPrefab.UnderlyingValue, parent);
            ICellPresenter cellPresenter = new CellPresenter(cellModel, cellView);
            cellPresenter.Init();
            allCells.Add(cellPresenter);
            return cellView;
        }
        public Dictionary<ICell_Readonly, ICellView> SpawnGameBoard(IGameBoard_Readonly gameBoard)
        {
            int xLength = gameBoard.Cells_Readonly.GetLength(0);
            int yLength = gameBoard.Cells_Readonly.GetLength(1);
            Dictionary<ICell_Readonly, ICellView> spawnedCells = new();

            for (int x = 0; x < xLength; x++)
            {
                for (int y = 0; y < yLength; y++)
                {
                    ICell_Readonly cellModel = gameBoard.Cells_Readonly[x, y];
                    spawnedCells[cellModel] = SpawnCell(cellModel);
                }
            }

            return spawnedCells;
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
                Destroy(transform.GetChild(i).gameObject);
            }
        }
    }
}