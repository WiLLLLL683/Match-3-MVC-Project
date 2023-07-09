using Presenter;
using Data;
using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using View;

namespace Presenter
{
    public class CellSpawner : MonoBehaviour
    {
        [SerializeField] private Transform parent;
        [SerializeField] private CellView cellPrefab;
        //[SerializeField] private AllBlockTypes allBlockTypes;

        private List<CellView> allCells = new();

        public CellView SpawnCell(Model.Objects.Cell _cellModel)
        {
            CellView cell = Instantiate(cellPrefab, parent);
            cell.Init(_cellModel);
            allCells.Add(cell);
            return cell;
        }
        public List<CellView> SpawnGameboard(Model.Objects.GameBoard gameBoard)
        {
            List<CellView> spawnedCells = new();
            CellView cell;

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
                Destroy(allCells[i].gameObject);
            }

            //уничтожить неучтенные объекты
            for (int i = 0; i < parent.childCount; i++)
            {
                Destroy(transform.GetChild(i).gameObject);
            }
        }
    }
}