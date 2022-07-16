using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model.Objects
{
    public class Cell
    {
        public bool isPlayable { get { return type.canContainBlock; } }
        public bool isEmpty { get { return CheckEmpty(); } }
        public ACellType type { get; private set; }
        public Block block { get; private set; }

        public event CellDelegate OnEmptyEvent;

        public event CellDelegate OnCellDestroyEvent;

        public Cell(ACellType _type)
        {
            type = _type;
        }

        public void SetBlock(Block _block)
        {
            if (isPlayable)
            {
                if (_block != null)
                {
                    block = _block;
                }
                else
                {
                    SetEmpty();
                }
            }
        }

        public void DestroyBlock()
        {
            if (isPlayable && block != null)
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
                OnCellDestroyEvent?.Invoke(this, new EventArgs());
            }
        }



        private void SetEmpty()
        {
            block = null;
            OnEmptyEvent?.Invoke(this, new EventArgs());
        }

        private bool CheckEmpty()
        {
            if (isPlayable && block == null)
            {
                OnEmptyEvent?.Invoke(this,new EventArgs()); //TODO BUG ивент вызывается даже при простой проверке на пустую клетку
                return true;
            }
            return false;
        }
    }
}
