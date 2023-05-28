using Data;
using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace View
{
    public class CellSpawner : MonoBehaviour
    {
        [SerializeField] private Transform parent;
        [SerializeField] private Cell cellPrefab;
        //[SerializeField] private AllBlockTypes allBlockTypes;

        private List<Cell> allCells = new();

        public Cell SpawnCell(Model.Objects.Cell _cellModel)
        {
            Cell cell = Instantiate(cellPrefab, parent);
            cell.Init(_cellModel);
            allCells.Add(cell);
            return cell;
        }
        public List<Cell> SpawnGameboard(Model.Objects.GameBoard gameBoard)
        {
            List<Cell> spawnedCells = new();
            Cell cell;

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