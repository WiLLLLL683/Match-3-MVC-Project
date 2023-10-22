using System;
using UnityEngine;
using Model.Readonly;

namespace Model.Objects
{
    /// <summary>
    /// Модель для клетки игрового поля, которая хранит в себе блок
    /// </summary>
    [Serializable]
    public class Cell
    {
        public CellType Type;
        public Vector2Int Position;
        public Block Block;

        public event Action<Cell> OnDestroy;

        public Cell(CellType type, Vector2Int position)
        {
            Type = type;
            Position = position;
        }

        /// <summary>
        /// Уничтожить материал клетки, результат зависит от типа клетки
        /// </summary>
        public void Destroy()
        {
            if (Type == null)
                return;

            Type.DestroyCellMaterial();
            OnDestroy?.Invoke(this);
        }
    }
}
