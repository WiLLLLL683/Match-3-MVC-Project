using System;
using UnityEngine;
using Model.Objects;
using Config;

namespace Model.Factories
{
    public class CellFactory : ICellFactory
    {
        private readonly CellType hiddenCellType;

        public CellFactory(IConfigProvider configProvider)
        {
            hiddenCellType = configProvider.HiddenCellType.type;
        }

        public Cell Create(Vector2Int position, CellType type) => new Cell(type.Clone(), position);
        public Cell CreateHidden(Vector2Int position) => new Cell(hiddenCellType.Clone(), position);
    }
}