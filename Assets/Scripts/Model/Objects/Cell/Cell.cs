using Model.Readonly;
using System;
using UnityEngine;

namespace Model.Objects
{
    /// <summary>
    /// Модель для клетки игрового поля, которая хранит в себе блок
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
        public void SetType(ICellType type)
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

        /// <summary>
        /// Уничтожить блок в клетке при возможности
        /// </summary>
        public void DestroyBlock()
        {
            if (!Type.CanContainBlock || Block == null)
                return;

            Block.Destroy();
            SetEmpty();
        }

        private void RegisterBlock(Block block)
        {
            Block = block;
            Block.SetPosition(Position);
        }

        private void SetEmpty()
        {
            Block = null;
            OnEmpty?.Invoke(this);
        }
    }
}
