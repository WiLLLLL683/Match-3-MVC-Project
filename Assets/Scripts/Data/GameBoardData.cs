using Model.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Data
{
    [System.Serializable]
    public struct GameBoardData
    {
        public Vector2Int gameBoardSize;
        public bool[,] playbleCells; //TODO сделать неиграбельные клетки через тип клетки
        public ACellType[,] cellTypes;
    }
}
