using System;
using UnityEngine;
using Model.Readonly;

namespace Model.Objects
{
    /// <summary>
    /// Модель для клетки игрового поля, которая хранит в себе блок
    /// </summary>
    [Serializable]
    public class Cell : ICell_Readonly
    {
        public CellType Type { get; private set; }
        public Vector2Int Position { get; private set; }
        public Block Block { get; private set; }
        public IBlock_Readonly Block_Readonly => Block;
        public ICellType_Readonly Type_Readonly => Type;

        public event Action<ICell_Readonly> OnEmpty;
        public event Action<ICell_Readonly> OnDestroy;
        public event Action<CellType> OnTypeChange;

        public Cell(CellType type, Vector2Int position)
        {
            Type = type;
            Position = position;
        }

        /// <summary>
        /// Изменить тип клетки
        /// </summary>
        public void SetType(CellType type)
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
                Block = block;
                Block.SetPosition(Position);
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
            if (Type == null)
                return;

            Type.DestroyCellMaterial();
            OnDestroy?.Invoke(this);
        }

        public void SetEmpty()
        {
            Block = null;
            OnEmpty?.Invoke(this);
        }
    }
}
