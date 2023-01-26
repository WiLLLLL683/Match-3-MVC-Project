using System;
using UnityEngine;
using Model.Objects;

namespace Data
{
    public class GameBoardData
    {
        public ACellType[,] cellTypes;

        public GameBoardData(int xLength, int yLength)
        {
            cellTypes = new ACellType[xLength, yLength]; 
            
            for (int i = 0; i < xLength; i++)
            {
                for (int j = 0; j < yLength; j++)
                {
                    cellTypes[i, j] = new BasicCellType();
                }
            }
        }

        public bool ValidCheck()
        {
            if (cellTypes == null || cellTypes.Length == 0)
            {
                Debug.LogError("Something wrong with GameBoardData");
                return false;
            }
            return true;
        }
    }
}