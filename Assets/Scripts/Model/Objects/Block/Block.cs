using System;
using UnityEngine;
using Utils;
namespace Model.Objects
{
    /// <summary>
    /// Модель для блока на игровом поле,
    /// Блок должен находиться в Клетке
    /// </summary>
    [Serializable]
    public class Block
    {
        public IBlockType Type;
        public Vector2Int Position;
        public bool isMarkedToDestroy;

        public Block(IBlockType type, Vector2Int position)
        {
            Type = type;
            Position = position;
        }
    }
}