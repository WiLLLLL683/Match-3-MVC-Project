using Data;
using Model.Readonly;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model.Objects
{
    /// <summary>
    /// Объект клетки игрового поля, которая хранит в себе блок
    /// </summary>
    [Serializable]
    public class Cell : ICell_Readonly
    {
        public bool IsPlayable => Type.IsPlayable;
        public bool CanContainBlock => Type.CanContainBlock;
        public bool IsEmpty { get; private set; }
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
            IsEmpty = true;
            Type = type;
            Position = position;
        }

        /// <summary>
        /// Изменить тип клетки
        /// </summary>
        public void ChangeType(ICellType type)
        {
            Type = type;
            OnTypeChange?.Invoke(Type);
        }

        /// <summary>
        /// Поместить блок в клетку при возможности, null для опустошения клетки
        /// </summary>
        public void SetBlock(Block block)
        {
            if (CanContainBlock)
            {
                if (block != null)
                {
                    Block = block;
                    Block.ChangePosition(this);
                    IsEmpty = false;
                }
                else
                {
                    SetEmpty();
                }
            }
        }

        /// <summary>
        /// Заспавнить блок в клетке при возможности
        /// </summary>
        public Block SpawnBlock(IBlockType blockType)
        {
            if (CanContainBlock && IsEmpty)
            {
                Block block = new Block(blockType, this);
                SetBlock(block);
                return block;
            }

            return null;
        }

        /// <summary>
        /// Уничтожить блок в клетке при возможности
        /// </summary>
        public void DestroyBlock()
        {
            if (CanContainBlock && Block != null)
            {
                Block.Destroy();
                SetEmpty();
            }
        }

        /// <summary>
        /// Уничтожить саму клетку, результат зависит от типа клетки
        /// </summary>
        public void DestroyCell()
        {
            if (Type != null)
            {
                Type.DestroyCellMaterial();
                OnDestroy?.Invoke(this);
            }
        }



        private void SetEmpty()
        {
            Block = null;
            IsEmpty = true;
            OnEmpty?.Invoke(this);
        }
    }
}
