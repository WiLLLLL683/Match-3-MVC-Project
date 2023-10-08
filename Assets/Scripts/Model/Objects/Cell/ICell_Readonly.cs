using Model.Objects;
using System;
using UnityEngine;

namespace Model.Readonly
{
    /// <summary>
    /// Readonly досуп для презентеров, изменение модели должно происходить через игровую логику
    /// </summary>
    public interface ICell_Readonly
    {
        public IBlock_Readonly Block_Readonly { get; }
        public Vector2Int Position { get; }
        public ICellType_Readonly Type_Readonly { get; }

        public event Action<ICell_Readonly> OnDestroy;
        public event Action<ICell_Readonly> OnEmpty;
        public event Action<CellType> OnTypeChange;
    }
}