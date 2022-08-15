using System;
using UnityEngine;
using Model.Objects;

namespace Data
{
    [System.Serializable]
    public struct GameBoardData
    {
        public ACellType[,] cellTypes;

        public GameBoardData(int xLength, int yLength)
        {
            cellTypes = new ACellType[xLength, yLength];
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