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
        public bool IsPlayable => Type.CanContainBlock;
        public bool IsEmpty { get; private set; }
        public ACellType Type { get; private set; }
        public Block Block { get; private set; }
        public IBlock_Readonly Block_Readonly => Block;
        public Vector2Int Position { get; private set; }

        public event Action<ICell_Readonly> OnEmpty;
        public event Action<ICell_Readonly> OnDestroy;
        public event Action<ACellType> OnTypeChange;

        public Cell(ACellType _type, Vector2Int _position)
        {
            IsEmpty = true;
            Type = _type;
            Position = _position;
        }

        /// <summary>
        /// Изменить тип клетки
        /// </summary>
        public void ChangeType(ACellType _type)
        {
            Type = _type;
            OnTypeChange?.Invoke(Type);
        }

        /// <summary>
        /// Поместить блок в клетку при возможности, null для опустошения клетки
        /// </summary>
        public void SetBlock(Block _block)
        {
            if (IsPlayable)
            {
                if (_block != null)
                {
                    Block = _block;
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
        public Block SpawnBlock(IBlockType _blockType)
        {
            if (IsPlayable && IsEmpty)
            {
                Block block = new Block(_blockType, this);
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
            if (IsPlayable && Block != null)
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
