using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model.Objects
{
    public class Cell
    {
        public bool IsPlayable { get { return type.canContainBlock; } }
        public bool isEmpty { get { return _isEmpty; } }
        private bool _isEmpty = true;
        public ACellType type { get; private set; }
        public Block block { get; private set; }

        public event CellDelegate OnEmptyEvent;

        public event CellDelegate OnCellDestroyEvent;

        public Cell(ACellType _type)
        {
            type = _type;
        }

        public void SetType(ACellType _type)
        {
            type = _type;
        }

        public void SetBlock(Block _block)
        {
            if (IsPlayable)
            {
                if (_block != null)
                {
                    block = _block;
                    _isEmpty = false;
                }
                else
                {
                    SetEmpty();
                }
            }
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
                OnCellDestroyEvent?.Invoke(this, new EventArgs());
            }
        }



        private void SetEmpty()
        {
            block = null;
            _isEmpty = true;
            OnEmptyEvent?.Invoke(this, new EventArgs());
        }

        //private bool CheckEmpty()
        //{
        //    if (IsPlayable && block == null)
        //    {
        //        OnEmptyEvent?.Invoke(this,new EventArgs()); //TODO BUG ивент вызывается даже при простой проверке на пустую клетку
        //        return true;
        //    }
        //    return false;
        //}
    }
}
