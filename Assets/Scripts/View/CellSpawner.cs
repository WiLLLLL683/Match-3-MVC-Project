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

        public Cell SpawnBlock(Model.Objects.Cell _cellModel)
        {
            Cell cell = Instantiate(cellPrefab, parent);
            cell.Init(_cellModel);
            allCells.Add(cell);
            return cell;
        }
    }
}