using Presenter;
using Data;
using Model.Objects;
using View;
using NaughtyAttributes;
using System;
using System.Collections;
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

        public ICellView SpawnCell(Model.Objects.Cell cellModel)
        {
            ICellView cellView = (ICellView)Instantiate(cellPrefab.UnderlyingValue, parent);
            ICellPresenter cellPresenter = new CellPresenter(cellModel, cellView);
            cellPresenter.Init();
            allCells.Add(cellPresenter);
            return cellView;
        }
        public Dictionary<Cell, ICellView> SpawnGameBoard(Model.Objects.GameBoard gameBoard)
        {
            int xLength = gameBoard.Cells.GetLength(0);
            int yLength = gameBoard.Cells.GetLength(1);
            Dictionary<Cell, ICellView> spawnedCells = new();

            for (int x = 0; x < xLength; x++)
            {
                for (int y = 0; y < yLength; y++)
                {
                    Cell cellModel = gameBoard.Cells[x, y];
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