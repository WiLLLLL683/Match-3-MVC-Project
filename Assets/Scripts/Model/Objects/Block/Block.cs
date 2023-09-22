using Model.Readonly;
using System;
using UnityEngine;

namespace Model.Objects
{
    /// <summary>
    /// Объект игрового блока
    /// </summary>
    [System.Serializable]
    public class Block : IBlock_Readonly
    {
        public IBlockType Type { get; private set; }
        public Cell Cell { get; private set; }
        public IBlockType_Readonly Type_Readonly => Type;
        public ICell_Readonly Cell_Readonly => Cell;
        public Vector2Int Position => Cell.Position;

        public event Action<Block> OnDestroy;
        public event Action<IBlock_Readonly> OnDestroy_Readonly;
        public event Action<IBlockType_Readonly> OnTypeChange;
        public event Action<Vector2Int> OnPositionChange;

        public Block(IBlockType type, Cell cell)
        {
            Type = type;
            Cell = cell;
        }

        /// <summary>
        /// Задать клетку в которой расположен блок
        /// </summary>
        public void ChangePosition(Cell cell)
        {
            Cell = cell;
            OnPositionChange?.Invoke(Position);
        }

        /// <summary>
        /// Изменить тип блока
        /// </summary>
        public void ChangeType(IBlockType type)
        {
            Type = type;
            OnTypeChange?.Invoke(type);
        }

        /// <summary>
        /// Активировать блок, зависит от типа блока
        /// </summary>
        public bool Activate() => Type.Activate();

        /// <summary>
        /// уничтожить блок
        /// </summary>
        public void Destroy()
        {
            OnDestroy?.Invoke(this);
            OnDestroy_Readonly?.Invoke(this);
        }
    }
}