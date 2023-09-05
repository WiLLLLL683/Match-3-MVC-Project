using Model.Objects;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Model.Objects
{
    public class HintPattern : Pattern
    {
        public Vector2Int cellToMove;
        public Directions directionToMove;

        public HintPattern(bool[,] grid, Vector2Int cellToMove, Directions directionToMove) : base(grid)
        {
            this.cellToMove = cellToMove;
            this.directionToMove = directionToMove;
        }
    }
}