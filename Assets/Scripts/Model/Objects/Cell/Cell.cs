using Model.Readonly;
using System;
using UnityEngine;

namespace Model.Objects
{
    /// <summary>
    /// Объект клетки игрового поля, которая хранит в себе блок
    /// </summary>
    [Serializable]
    public class Cell : ICell_Readonly
    {
        public ICellType Type { get; private set; }
        public Vector2Int Position { get; private set; }
        public Block Block { get; private set; }
        public IBlock_Readonly Block_Readonly => Block;
        public ICellType_Readonly Type_Readonly => Type;

        public event Action<ICell_Readonly> OnEmpty;
        public event Action<ICell_Readonly> OnDestroy;
        public event Action<ICellType> OnTypeChange;

        public Cell(ICellType type, Vector2Int position)
        {
            Type = type;
            Position = position;
        }

        /// <summary>
        /// Изменить тип клетки
        /// </summary>
        public void ChangeType(ICellType type)
        {
            if (type == null)
                return;

            Type = type;
            OnTypeChange?.Invoke(Type);
        }

        /// <summary>
        /// Поместить блок в клетку при возможности, null для опустошения клетки
        /// </summary>
        public void SetBlock(Block block)
        {
            if (!Type.CanContainBlock)
                return;

            if (block != null)
            {
                RegisterBlock(block);
            }
            else
            {
                SetEmpty();
            }
        }

        /// <summary>
        /// Уничтожить материал клетки, результат зависит от типа клетки
        /// </summary>
        public void Destroy()
        {
            if (Type != null)
            {
                Type.DestroyCellMaterial();
                OnDestroy?.Invoke(this);
            }
        }

        private void RegisterBlock(Block block)
        {
            Block = block;
            Block.ChangePosition(this);
            Block.OnDestroy += SetEmpty;
        }

        private void SetEmpty(Block _) => SetEmpty();

        private void SetEmpty()
        {
            if (Block != null)
            {
                Block.OnDestroy -= SetEmpty;
            }

            Block = null;
            OnEmpty?.Invoke(this);
        }
    }
}
