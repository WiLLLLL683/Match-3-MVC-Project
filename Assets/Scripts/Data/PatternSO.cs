using Array2DEditor;
using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;
using Model.Objects;
using System;
using Model.Services;

namespace Data
{
    [CreateAssetMenu(fileName ="Pattern", menuName ="Config/Pattern")]
    public class PatternSO: ScriptableObject
    {
        [SerializeField] protected Array2DBool array2d;

        public bool[,] GetBoolGrid()
        {
            bool[,] grid = new bool[array2d.GridSize.x, array2d.GridSize.y];

            for (int i = 0; i < array2d.GridSize.x; i++)
            {
                for (int j = 0; j < array2d.GridSize.y; j++)
                {
                    grid[i, j] = array2d.GetCell(i, j);
                }
            }

            return grid;
        }
    }
}