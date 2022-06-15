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
    }
}
