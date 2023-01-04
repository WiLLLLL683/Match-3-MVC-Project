using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model.Objects
{
    /// <summary>
    /// Объект клетки игрового поля, которая хранит в себе блок
    /// </summary>
    public class Cell
    {
        public bool IsPlayable { get { return type.canContainBlock; } }
        public bool isEmpty { get; private set; }
        public ACellType type { get; private set; }
        public Block block { get; private set; }
        public Vector2Int position { get; private set; }

        public event CellDelegate OnEmpty;
        public event CellDelegate OnDestroy;
        public event CellDelegate OnTypeChange;

        public Cell(ACellType _type, Vector2Int _position)
        {
            isEmpty = true;
            type = _type;
            position = _position;
        }

        public void ChangeType(ACellType _type)
        {
            type = _type;
            OnTypeChange?.Invoke(this, new EventArgs());
        }

        public void SetBlock(Block _block)
        {
            if (IsPlayable)
            {
                if (_block != null)
                {
                    block = _block;
                    block.ChangePosition(this);
                    isEmpty = false;
                }
                else
                {
                    SetEmpty();
                }
            }
        }

        public Block SpawnBlock(ABlockType _blockType)
        {
            if (IsPlayable && isEmpty)
            {
                Block block = new Block(_blockType,this);
                SetBlock(block);
                return block;
            }

            return null;
        }

        public void DestroyBlock()
        {
            if (IsPlayable && block != null)
            {
                block.Destroy();
                SetEmpty();
            }
        }

        public void DestroyCell()
        {
            if (type != null)
            {
                type.DestroyCellMaterial();
                OnDestroy?.Invoke(this, new EventArgs());
            }
        }



        private void SetEmpty()
        {
            block = null;
            isEmpty = true;
            OnEmpty?.Invoke(this, new EventArgs());
        }
    }
}
