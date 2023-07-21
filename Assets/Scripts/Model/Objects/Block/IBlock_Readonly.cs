using Data;
using System;
using UnityEngine;

namespace Model.Readonly
{
    /// <summary>
    /// Readonly досуп для презентеров, изменение модели должно происходить через игровую логику
    /// </summary>
    public interface IBlock_Readonly
    {
        public Vector2Int Position { get; }
        public ABlockType Type { get; }
        public ICell_Readonly Cell_Readonly { get; }

        public event Action<IBlock_Readonly> OnDestroy_Readonly;
        public event Action<ABlockType> OnTypeChange;
        public event Action<Vector2Int> OnPositionChange;
    }
}