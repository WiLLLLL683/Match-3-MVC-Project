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

        public Cell(CellType type, Vector2Int position)
        {
            Type = type;
            Position = position;
        }
    }
}
