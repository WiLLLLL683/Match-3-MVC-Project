using Presenter;
using Data;
using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using View;
using AYellowpaper;

namespace Presenter
{
    public class CellSpawner : MonoBehaviour
    {
        [SerializeField] private Transform parent;
        [SerializeField] private InterfaceReference<ICellView, MonoBehaviour> cellPrefab;

        private List<ICellPresenter> allCells = new();

        public ICellPresenter SpawnCell(Model.Objects.Cell cellModel)
        {
            ICellView cellView = (ICellView)Instantiate(cellPrefab.UnderlyingValue, parent);
            ICellPresenter cellPresenter = new CellPresenter(cellModel, cellView);
            cellPresenter.Init();
            allCells.Add(cellPresenter);
            return cellPresenter;
        }
        public List<ICellPresenter> SpawnGameBoard(Model.Objects.GameBoard gameBoard)
        {
            List<ICellPresenter> spawnedCells = new();
            ICellPresenter cell;

            for (int x = 0; x < gameBoard.Cells.GetLength(0); x++)
            {
                for (int y = 0; y < gameBoard.Cells.GetLength(1); y++)
                {
                    cell = SpawnCell(gameBoard.Cells[x, y]);
                    spawnedCells.Add(cell);
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