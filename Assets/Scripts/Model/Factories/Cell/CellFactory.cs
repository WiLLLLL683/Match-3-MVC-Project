using System;
using UnityEngine;
using Model.Objects;

namespace Model.Factories
{
    public class CellFactory : ICellFactory
    {
        private readonly CellType invisibleCellType;

        public CellFactory(CellType invisibleCellType)
        {
            this.invisibleCellType = invisibleCellType;
        }

        public Cell Create(Vector2Int position, CellType type) => new Cell(type, position);
        public Cell CreateInvisible(Vector2Int position) => new Cell(invisibleCellType, position);
    }
}