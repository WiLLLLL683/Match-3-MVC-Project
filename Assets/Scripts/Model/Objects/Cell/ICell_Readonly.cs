using Data;
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
        public bool IsEmpty { get; }
        public bool IsPlayable { get; }
        public Vector2Int Position { get; }
        public ACellType Type { get; }

        public event Action<ICell_Readonly> OnDestroy;
        public event Action<ICell_Readonly> OnEmpty;
        public event Action<ACellType> OnTypeChange;
    }
}